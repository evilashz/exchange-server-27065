using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.Prompts.Provisioning;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D28 RID: 3368
	[Cmdlet("Import", "UMPrompt", DefaultParameterSetName = "UploadDialPlanPrompts", SupportsShouldProcess = true)]
	public sealed class ImportUMPrompt : UMPromptTaskBase<UMDialPlanIdParameter>
	{
		// Token: 0x17002810 RID: 10256
		// (get) Token: 0x0600812A RID: 33066 RVA: 0x00210815 File Offset: 0x0020EA15
		// (set) Token: 0x0600812B RID: 33067 RVA: 0x0021081D File Offset: 0x0020EA1D
		private new UMDialPlanIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17002811 RID: 10257
		// (get) Token: 0x0600812C RID: 33068 RVA: 0x00210826 File Offset: 0x0020EA26
		// (set) Token: 0x0600812D RID: 33069 RVA: 0x0021083D File Offset: 0x0020EA3D
		[Parameter(Mandatory = true, ParameterSetName = "UploadAutoAttendantPromptsStream")]
		[Parameter(Mandatory = true, ParameterSetName = "UploadAutoAttendantPrompts")]
		[ValidateNotNullOrEmpty]
		public override UMAutoAttendantIdParameter UMAutoAttendant
		{
			get
			{
				return (UMAutoAttendantIdParameter)base.Fields["UMAutoAttendant"];
			}
			set
			{
				base.Fields["UMAutoAttendant"] = value;
			}
		}

		// Token: 0x17002812 RID: 10258
		// (get) Token: 0x0600812E RID: 33070 RVA: 0x00210850 File Offset: 0x0020EA50
		// (set) Token: 0x0600812F RID: 33071 RVA: 0x00210858 File Offset: 0x0020EA58
		[Parameter(Mandatory = true, ParameterSetName = "UploadDialPlanPromptsStream")]
		[Parameter(Mandatory = true, ParameterSetName = "UploadDialPlanPrompts")]
		[ValidateNotNullOrEmpty]
		public override UMDialPlanIdParameter UMDialPlan
		{
			get
			{
				return this.Identity;
			}
			set
			{
				this.Identity = value;
			}
		}

		// Token: 0x17002813 RID: 10259
		// (get) Token: 0x06008130 RID: 33072 RVA: 0x00210861 File Offset: 0x0020EA61
		// (set) Token: 0x06008131 RID: 33073 RVA: 0x00210878 File Offset: 0x0020EA78
		[Parameter(Mandatory = true, ParameterSetName = "UploadDialPlanPrompts")]
		[Parameter(Mandatory = true, ParameterSetName = "UploadDialPlanPromptsStream")]
		[Parameter(Mandatory = true, ParameterSetName = "UploadAutoAttendantPromptsStream")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "UploadAutoAttendantPrompts")]
		public string PromptFileName
		{
			get
			{
				return (string)base.Fields["PromptFileName"];
			}
			set
			{
				base.Fields["PromptFileName"] = value;
			}
		}

		// Token: 0x17002814 RID: 10260
		// (get) Token: 0x06008132 RID: 33074 RVA: 0x0021088B File Offset: 0x0020EA8B
		// (set) Token: 0x06008133 RID: 33075 RVA: 0x002108A2 File Offset: 0x0020EAA2
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "UploadDialPlanPrompts")]
		[Parameter(Mandatory = true, ParameterSetName = "UploadAutoAttendantPrompts")]
		public byte[] PromptFileData
		{
			get
			{
				return (byte[])base.Fields["PromptFileData"];
			}
			set
			{
				base.Fields["PromptFileData"] = value;
			}
		}

		// Token: 0x17002815 RID: 10261
		// (get) Token: 0x06008134 RID: 33076 RVA: 0x002108B5 File Offset: 0x0020EAB5
		// (set) Token: 0x06008135 RID: 33077 RVA: 0x002108CC File Offset: 0x0020EACC
		[Parameter(Mandatory = true, ParameterSetName = "UploadAutoAttendantPromptsStream")]
		[Parameter(Mandatory = true, ParameterSetName = "UploadDialPlanPromptsStream")]
		[ValidateNotNullOrEmpty]
		public Stream PromptFileStream
		{
			get
			{
				return (Stream)base.Fields["PromptFileStream"];
			}
			set
			{
				base.Fields["PromptFileStream"] = value;
			}
		}

		// Token: 0x17002816 RID: 10262
		// (get) Token: 0x06008136 RID: 33078 RVA: 0x002108E0 File Offset: 0x0020EAE0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("UploadAutoAttendantPrompts" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageImportUMPromptAutoAttendantPrompts(this.PromptFileName, this.UMAutoAttendant.ToString());
				}
				return Strings.ConfirmationMessageImportUMPromptDialPlanPrompts(this.PromptFileName, this.UMDialPlan.ToString());
			}
		}

		// Token: 0x06008137 RID: 33079 RVA: 0x0021092C File Offset: 0x0020EB2C
		protected override void InternalValidate()
		{
			if (!ImportUMPrompt.IsDesktopExperienceRoleInstalled())
			{
				ServerIdParameter serverIdParameter = new ServerIdParameter();
				base.WriteError(new DesktopExperienceRequiredException(serverIdParameter.Fqdn), ErrorCategory.NotInstalled, null);
			}
			base.InternalValidate();
			long num = (this.PromptFileData != null) ? ((long)this.PromptFileData.Length) : this.PromptFileStream.Length;
			if (num > 5242880L)
			{
				this.HandleOversizeAudioData();
			}
			if (this.PromptFileName.Length > 128 || !string.Equals(Path.GetFileName(this.PromptFileName), this.PromptFileName))
			{
				base.WriteError(new InvalidFileNameException(128), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06008138 RID: 33080 RVA: 0x002109CC File Offset: 0x0020EBCC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADConfigurationObject config = null;
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (parameterSetName == "UploadDialPlanPrompts" || parameterSetName == "UploadDialPlanPromptsStream")
				{
					config = this.DataObject;
					goto IL_7D;
				}
				if (parameterSetName == "UploadAutoAttendantPrompts" || parameterSetName == "UploadAutoAttendantPromptsStream")
				{
					config = base.AutoAttendant;
					goto IL_7D;
				}
			}
			ExAssert.RetailAssert(false, "Invalid parameter set {0}", new object[]
			{
				base.ParameterSetName
			});
			try
			{
				IL_7D:
				ITempFile tempFile = null;
				string extension = Path.GetExtension(this.PromptFileName);
				if (string.Equals(extension, ".wav", StringComparison.OrdinalIgnoreCase))
				{
					tempFile = TempFileFactory.CreateTempWavFile();
				}
				else
				{
					if (!string.Equals(extension, ".wma", StringComparison.OrdinalIgnoreCase))
					{
						throw new InvalidFileNameException(128);
					}
					tempFile = TempFileFactory.CreateTempWmaFile();
				}
				using (tempFile)
				{
					using (FileStream fileStream = new FileStream(tempFile.FilePath, FileMode.Create, FileAccess.Write))
					{
						if (this.PromptFileData != null)
						{
							fileStream.Write(this.PromptFileData, 0, this.PromptFileData.Length);
						}
						else
						{
							CommonUtil.CopyStream(this.PromptFileStream, fileStream);
						}
					}
					using (IPublishingSession publishingSession = PublishingPoint.GetPublishingSession(Environment.UserName, config))
					{
						publishingSession.Upload(tempFile.FilePath, this.PromptFileName);
					}
				}
			}
			catch (UnsupportedCustomGreetingSizeFormatException)
			{
				this.HandleOversizeAudioData();
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, (ErrorCategory)1000, null);
			}
			catch (SystemException ex)
			{
				if (!this.HandleException(ex))
				{
					throw;
				}
				base.WriteError(ex, (ErrorCategory)1000, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008139 RID: 33081 RVA: 0x00210BB0 File Offset: 0x0020EDB0
		private void HandleOversizeAudioData()
		{
			base.WriteError(new AudioDataIsOversizeException(5, 5L), ErrorCategory.NotSpecified, null);
		}

		// Token: 0x0600813A RID: 33082 RVA: 0x00210BC2 File Offset: 0x0020EDC2
		private bool HandleException(SystemException e)
		{
			return e is IOException || e is SecurityException || e is NotSupportedException || e is UnauthorizedAccessException;
		}

		// Token: 0x0600813B RID: 33083 RVA: 0x00210BE8 File Offset: 0x0020EDE8
		private static bool IsDesktopExperienceRoleInstalled()
		{
			string systemDirectory = Environment.SystemDirectory;
			return !string.IsNullOrEmpty(systemDirectory) && Environment.OSVersion.Version.Major >= 6 && ImportUMPrompt.CheckVersionInfo(Path.Combine(systemDirectory, "wmvcore.dll"), 3802) && ImportUMPrompt.CheckVersionInfo(Path.Combine(systemDirectory, "wmspdmod.dll"), 3804) && ImportUMPrompt.CheckVersionInfo(Path.Combine(systemDirectory, "wmspdmoe.dll"), 3804);
		}

		// Token: 0x0600813C RID: 33084 RVA: 0x00210C60 File Offset: 0x0020EE60
		private static bool CheckVersionInfo(string fullPath, int revisionNumber)
		{
			try
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(fullPath);
				if (versionInfo == null || string.IsNullOrEmpty(versionInfo.FileVersion) || (versionInfo.FileVersion.StartsWith("10.0.0") && versionInfo.FilePrivatePart < revisionNumber))
				{
					return false;
				}
				return true;
			}
			catch (FileNotFoundException)
			{
			}
			return false;
		}

		// Token: 0x02000D29 RID: 3369
		internal abstract class ParameterSet
		{
			// Token: 0x04003F1E RID: 16158
			internal const string UploadDialPlanPrompts = "UploadDialPlanPrompts";

			// Token: 0x04003F1F RID: 16159
			internal const string UploadAutoAttendantPrompts = "UploadAutoAttendantPrompts";

			// Token: 0x04003F20 RID: 16160
			internal const string UploadDialPlanPromptsStream = "UploadDialPlanPromptsStream";

			// Token: 0x04003F21 RID: 16161
			internal const string UploadAutoAttendantPromptsStream = "UploadAutoAttendantPromptsStream";
		}
	}
}
