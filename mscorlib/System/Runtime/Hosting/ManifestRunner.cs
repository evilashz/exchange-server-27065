using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Hosting
{
	// Token: 0x02000A28 RID: 2600
	internal sealed class ManifestRunner
	{
		// Token: 0x060065A2 RID: 26018 RVA: 0x001551AC File Offset: 0x001533AC
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		internal ManifestRunner(AppDomain domain, ActivationContext activationContext)
		{
			this.m_domain = domain;
			string text;
			string text2;
			CmsUtils.GetEntryPoint(activationContext, out text, out text2);
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoMain"));
			}
			if (string.IsNullOrEmpty(text2))
			{
				this.m_args = new string[0];
			}
			else
			{
				this.m_args = text2.Split(new char[]
				{
					' '
				});
			}
			this.m_apt = ApartmentState.Unknown;
			string applicationDirectory = activationContext.ApplicationDirectory;
			this.m_path = Path.Combine(applicationDirectory, text);
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x060065A3 RID: 26019 RVA: 0x00155230 File Offset: 0x00153430
		internal RuntimeAssembly EntryAssembly
		{
			[SecurityCritical]
			[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			get
			{
				if (this.m_assembly == null)
				{
					this.m_assembly = (RuntimeAssembly)Assembly.LoadFrom(this.m_path);
				}
				return this.m_assembly;
			}
		}

		// Token: 0x060065A4 RID: 26020 RVA: 0x0015525C File Offset: 0x0015345C
		[SecurityCritical]
		private void NewThreadRunner()
		{
			this.m_runResult = this.Run(false);
		}

		// Token: 0x060065A5 RID: 26021 RVA: 0x0015526C File Offset: 0x0015346C
		[SecurityCritical]
		private int RunInNewThread()
		{
			Thread thread = new Thread(new ThreadStart(this.NewThreadRunner));
			thread.SetApartmentState(this.m_apt);
			thread.Start();
			thread.Join();
			return this.m_runResult;
		}

		// Token: 0x060065A6 RID: 26022 RVA: 0x001552AC File Offset: 0x001534AC
		[SecurityCritical]
		private int Run(bool checkAptModel)
		{
			if (checkAptModel && this.m_apt != ApartmentState.Unknown)
			{
				if (Thread.CurrentThread.GetApartmentState() != ApartmentState.Unknown && Thread.CurrentThread.GetApartmentState() != this.m_apt)
				{
					return this.RunInNewThread();
				}
				Thread.CurrentThread.SetApartmentState(this.m_apt);
			}
			return this.m_domain.nExecuteAssembly(this.EntryAssembly, this.m_args);
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x00155314 File Offset: 0x00153514
		[SecurityCritical]
		internal int ExecuteAsAssembly()
		{
			object[] customAttributes = this.EntryAssembly.EntryPoint.GetCustomAttributes(typeof(STAThreadAttribute), false);
			if (customAttributes.Length != 0)
			{
				this.m_apt = ApartmentState.STA;
			}
			customAttributes = this.EntryAssembly.EntryPoint.GetCustomAttributes(typeof(MTAThreadAttribute), false);
			if (customAttributes.Length != 0)
			{
				if (this.m_apt == ApartmentState.Unknown)
				{
					this.m_apt = ApartmentState.MTA;
				}
				else
				{
					this.m_apt = ApartmentState.Unknown;
				}
			}
			return this.Run(true);
		}

		// Token: 0x04002D56 RID: 11606
		private AppDomain m_domain;

		// Token: 0x04002D57 RID: 11607
		private string m_path;

		// Token: 0x04002D58 RID: 11608
		private string[] m_args;

		// Token: 0x04002D59 RID: 11609
		private ApartmentState m_apt;

		// Token: 0x04002D5A RID: 11610
		private RuntimeAssembly m_assembly;

		// Token: 0x04002D5B RID: 11611
		private int m_runResult;
	}
}
