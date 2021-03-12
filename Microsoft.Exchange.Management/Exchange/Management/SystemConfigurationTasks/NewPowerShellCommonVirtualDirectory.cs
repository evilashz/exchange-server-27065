using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C29 RID: 3113
	public abstract class NewPowerShellCommonVirtualDirectory<T> : NewExchangeServiceVirtualDirectory<T> where T : ADPowerShellCommonVirtualDirectory, new()
	{
		// Token: 0x060075AE RID: 30126 RVA: 0x001E1008 File Offset: 0x001DF208
		public NewPowerShellCommonVirtualDirectory()
		{
			base.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
		}

		// Token: 0x17002432 RID: 9266
		// (get) Token: 0x060075AF RID: 30127 RVA: 0x001E1017 File Offset: 0x001DF217
		// (set) Token: 0x060075B0 RID: 30128 RVA: 0x001E101F File Offset: 0x001DF21F
		[Parameter(Mandatory = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17002433 RID: 9267
		// (get) Token: 0x060075B1 RID: 30129 RVA: 0x001E1028 File Offset: 0x001DF228
		// (set) Token: 0x060075B2 RID: 30130 RVA: 0x001E1030 File Offset: 0x001DF230
		internal new bool DigestAuthentication
		{
			get
			{
				return base.DigestAuthentication;
			}
			set
			{
				base.DigestAuthentication = value;
			}
		}

		// Token: 0x17002434 RID: 9268
		// (get) Token: 0x060075B3 RID: 30131 RVA: 0x001E1039 File Offset: 0x001DF239
		// (set) Token: 0x060075B4 RID: 30132 RVA: 0x001E1041 File Offset: 0x001DF241
		internal new string ApplicationRoot
		{
			get
			{
				return base.ApplicationRoot;
			}
			set
			{
				base.ApplicationRoot = value;
			}
		}

		// Token: 0x17002435 RID: 9269
		// (get) Token: 0x060075B5 RID: 30133 RVA: 0x001E104A File Offset: 0x001DF24A
		// (set) Token: 0x060075B6 RID: 30134 RVA: 0x001E104D File Offset: 0x001DF24D
		protected new ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
		{
			get
			{
				return ExtendedProtectionTokenCheckingMode.None;
			}
			set
			{
			}
		}

		// Token: 0x17002436 RID: 9270
		// (get) Token: 0x060075B7 RID: 30135 RVA: 0x001E104F File Offset: 0x001DF24F
		// (set) Token: 0x060075B8 RID: 30136 RVA: 0x001E1052 File Offset: 0x001DF252
		protected new MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17002437 RID: 9271
		// (get) Token: 0x060075B9 RID: 30137 RVA: 0x001E1054 File Offset: 0x001DF254
		// (set) Token: 0x060075BA RID: 30138 RVA: 0x001E1057 File Offset: 0x001DF257
		protected new MultiValuedProperty<string> ExtendedProtectionSPNList
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17002438 RID: 9272
		// (get) Token: 0x060075BB RID: 30139 RVA: 0x001E1059 File Offset: 0x001DF259
		// (set) Token: 0x060075BC RID: 30140 RVA: 0x001E1084 File Offset: 0x001DF284
		[Parameter(Mandatory = false)]
		public bool CertificateAuthentication
		{
			get
			{
				return base.Fields["CertificateAuthentication"] != null && (bool)base.Fields["CertificateAuthentication"];
			}
			set
			{
				base.Fields["CertificateAuthentication"] = value;
			}
		}

		// Token: 0x17002439 RID: 9273
		// (get) Token: 0x060075BD RID: 30141 RVA: 0x001E109C File Offset: 0x001DF29C
		// (set) Token: 0x060075BE RID: 30142 RVA: 0x001E10A4 File Offset: 0x001DF2A4
		[Parameter(Mandatory = false)]
		public new bool LimitMaximumMemory
		{
			get
			{
				return base.LimitMaximumMemory;
			}
			set
			{
				base.LimitMaximumMemory = value;
			}
		}

		// Token: 0x1700243A RID: 9274
		// (get) Token: 0x060075BF RID: 30143 RVA: 0x001E10AD File Offset: 0x001DF2AD
		protected override string VirtualDirectoryName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x060075C0 RID: 30144 RVA: 0x001E10B8 File Offset: 0x001DF2B8
		protected override void InternalProcessComplete()
		{
			base.InternalProcessComplete();
			if (this.CertificateAuthentication)
			{
				T dataObject = this.DataObject;
				dataObject.CertificateAuthentication = new bool?(true);
				ADExchangeServiceVirtualDirectory virtualDirectory = this.DataObject;
				Task.TaskErrorLoggingDelegate errorHandler = new Task.TaskErrorLoggingDelegate(base.WriteError);
				T dataObject2 = this.DataObject;
				ExchangeServiceVDirHelper.SetIisVirtualDirectoryAuthenticationMethods(virtualDirectory, errorHandler, Strings.ErrorUpdatingVDir(dataObject2.MetabasePath, string.Empty));
			}
		}
	}
}
