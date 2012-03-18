using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using NDoc.Core;

namespace NDoc.Gui
{
	/// <summary>
	/// Summary description for BuildWorker.
	/// </summary>
	public class BuildWorker
	{
		private IDocumenter m_documenter;
		private Project     m_project;
		private Exception   m_exception;
		private bool        m_isComplete = false;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="documenter"></param>
		/// <param name="project"></param>
		public BuildWorker(IDocumenter documenter, Project project)
		{
			m_documenter = documenter;
			m_project = project;
		}

		/// <summary>
		/// 
		/// </summary>
		public Exception Exception
		{
			get { return m_exception; }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsComplete
		{
			get { return m_isComplete; }
		}

		/// <summary>
		/// Tells the documenter to build the documentation for the given assembly
		/// /doc pairs and their namespace summaries.
		/// </summary>
		public void ThreadProc()
		{
			try
			{
				m_isComplete = false;

				GC.Collect();
				Debug.WriteLine("Memory before build: " + GC.GetTotalMemory(false).ToString());

				// Build the documentation.
				m_documenter.Build(m_project);

				GC.Collect();
				Debug.WriteLine("Memory after build: " + GC.GetTotalMemory(false).ToString());

				m_isComplete = true;
			}
			catch (Exception ex)
			{
				m_exception = ex;
			}
		}
	}
}
