using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.CabUtility;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001CE RID: 462
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("extract", "BundledDirectory", SupportsShouldProcess = true)]
	public sealed class ExtractBundledDirectory : Task
	{
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x00048154 File Offset: 0x00046354
		// (set) Token: 0x06001010 RID: 4112 RVA: 0x0004816B File Offset: 0x0004636B
		[Parameter(Mandatory = true)]
		public LongPath BundlePath
		{
			get
			{
				return (LongPath)base.Fields["BundlePath"];
			}
			set
			{
				this.bundlePath = value;
				base.Fields["BundlePath"] = this.bundlePath;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x0004818A File Offset: 0x0004638A
		// (set) Token: 0x06001012 RID: 4114 RVA: 0x000481A4 File Offset: 0x000463A4
		[Parameter(Mandatory = true)]
		public string DirToExtract
		{
			get
			{
				return (string)base.Fields["DirToExtract"];
			}
			set
			{
				string text = value;
				if (text[text.Length - 1] != '\\')
				{
					text += '\\';
				}
				base.Fields["DirToExtract"] = text;
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000481E4 File Offset: 0x000463E4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (!File.Exists(this.bundlePath.PathName) || Path.GetFileName(this.bundlePath.PathName).ToLower() != "languagepackbundle.exe")
			{
				base.WriteError(new TaskException(Strings.EBDInvalidBundle(this.bundlePath.PathName)), ErrorCategory.InvalidArgument, 0);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00048250 File Offset: 0x00046450
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			DirectoryInfo directoryInfo = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(this.BundlePath.PathName)));
			try
			{
				EmbeddedCabWrapper.ExtractFiles(this.BundlePath.PathName, directoryInfo.FullName, this.DirToExtract);
			}
			catch (CabUtilityWrapperException e)
			{
				TaskLogger.LogError(e);
				TaskLogger.LogExit();
			}
			string sendToPipeline = directoryInfo.FullName + '\\' + this.DirToExtract;
			base.WriteObject(sendToPipeline);
			TaskLogger.LogExit();
		}

		// Token: 0x04000767 RID: 1895
		private LongPath bundlePath;
	}
}
