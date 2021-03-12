using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
	// Token: 0x02000A2A RID: 2602
	[ComVisible(true)]
	[Serializable]
	public sealed class ActivationArguments : EvidenceBase
	{
		// Token: 0x060065AC RID: 26028 RVA: 0x001554FF File Offset: 0x001536FF
		private ActivationArguments()
		{
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x060065AD RID: 26029 RVA: 0x00155507 File Offset: 0x00153707
		internal bool UseFusionActivationContext
		{
			get
			{
				return this.m_useFusionActivationContext;
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x060065AE RID: 26030 RVA: 0x0015550F File Offset: 0x0015370F
		// (set) Token: 0x060065AF RID: 26031 RVA: 0x00155517 File Offset: 0x00153717
		internal bool ActivateInstance
		{
			get
			{
				return this.m_activateInstance;
			}
			set
			{
				this.m_activateInstance = value;
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x060065B0 RID: 26032 RVA: 0x00155520 File Offset: 0x00153720
		internal string ApplicationFullName
		{
			get
			{
				return this.m_appFullName;
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x060065B1 RID: 26033 RVA: 0x00155528 File Offset: 0x00153728
		internal string[] ApplicationManifestPaths
		{
			get
			{
				return this.m_appManifestPaths;
			}
		}

		// Token: 0x060065B2 RID: 26034 RVA: 0x00155530 File Offset: 0x00153730
		public ActivationArguments(ApplicationIdentity applicationIdentity) : this(applicationIdentity, null)
		{
		}

		// Token: 0x060065B3 RID: 26035 RVA: 0x0015553A File Offset: 0x0015373A
		public ActivationArguments(ApplicationIdentity applicationIdentity, string[] activationData)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			this.m_appFullName = applicationIdentity.FullName;
			this.m_activationData = activationData;
		}

		// Token: 0x060065B4 RID: 26036 RVA: 0x00155563 File Offset: 0x00153763
		public ActivationArguments(ActivationContext activationData) : this(activationData, null)
		{
		}

		// Token: 0x060065B5 RID: 26037 RVA: 0x00155570 File Offset: 0x00153770
		public ActivationArguments(ActivationContext activationContext, string[] activationData)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			this.m_appFullName = activationContext.Identity.FullName;
			this.m_appManifestPaths = activationContext.ManifestPaths;
			this.m_activationData = activationData;
			this.m_useFusionActivationContext = true;
		}

		// Token: 0x060065B6 RID: 26038 RVA: 0x001555BC File Offset: 0x001537BC
		internal ActivationArguments(string appFullName, string[] appManifestPaths, string[] activationData)
		{
			if (appFullName == null)
			{
				throw new ArgumentNullException("appFullName");
			}
			this.m_appFullName = appFullName;
			this.m_appManifestPaths = appManifestPaths;
			this.m_activationData = activationData;
			this.m_useFusionActivationContext = true;
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x060065B7 RID: 26039 RVA: 0x001555EE File Offset: 0x001537EE
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return new ApplicationIdentity(this.m_appFullName);
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x060065B8 RID: 26040 RVA: 0x001555FB File Offset: 0x001537FB
		public ActivationContext ActivationContext
		{
			get
			{
				if (!this.UseFusionActivationContext)
				{
					return null;
				}
				if (this.m_appManifestPaths == null)
				{
					return new ActivationContext(new ApplicationIdentity(this.m_appFullName));
				}
				return new ActivationContext(new ApplicationIdentity(this.m_appFullName), this.m_appManifestPaths);
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x060065B9 RID: 26041 RVA: 0x00155636 File Offset: 0x00153836
		public string[] ActivationData
		{
			get
			{
				return this.m_activationData;
			}
		}

		// Token: 0x060065BA RID: 26042 RVA: 0x00155640 File Offset: 0x00153840
		public override EvidenceBase Clone()
		{
			ActivationArguments activationArguments = new ActivationArguments();
			activationArguments.m_useFusionActivationContext = this.m_useFusionActivationContext;
			activationArguments.m_activateInstance = this.m_activateInstance;
			activationArguments.m_appFullName = this.m_appFullName;
			if (this.m_appManifestPaths != null)
			{
				activationArguments.m_appManifestPaths = new string[this.m_appManifestPaths.Length];
				Array.Copy(this.m_appManifestPaths, activationArguments.m_appManifestPaths, activationArguments.m_appManifestPaths.Length);
			}
			if (this.m_activationData != null)
			{
				activationArguments.m_activationData = new string[this.m_activationData.Length];
				Array.Copy(this.m_activationData, activationArguments.m_activationData, activationArguments.m_activationData.Length);
			}
			activationArguments.m_activateInstance = this.m_activateInstance;
			activationArguments.m_appFullName = this.m_appFullName;
			activationArguments.m_useFusionActivationContext = this.m_useFusionActivationContext;
			return activationArguments;
		}

		// Token: 0x04002D5C RID: 11612
		private bool m_useFusionActivationContext;

		// Token: 0x04002D5D RID: 11613
		private bool m_activateInstance;

		// Token: 0x04002D5E RID: 11614
		private string m_appFullName;

		// Token: 0x04002D5F RID: 11615
		private string[] m_appManifestPaths;

		// Token: 0x04002D60 RID: 11616
		private string[] m_activationData;
	}
}
