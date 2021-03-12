using System;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200005B RID: 91
	public abstract class GetRecipientObjectTask<TIdentity, TDataObject> : GetTenantADObjectWithIdentityTaskBase<TIdentity, TDataObject> where TIdentity : IIdentityParameter where TDataObject : ADObject, new()
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000E43A File Offset: 0x0000C63A
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0000E451 File Offset: 0x0000C651
		[Parameter]
		public PSCredential Credential
		{
			get
			{
				return (PSCredential)base.Fields["Credential"];
			}
			set
			{
				base.Fields["Credential"] = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000E464 File Offset: 0x0000C664
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x0000E46C File Offset: 0x0000C66C
		[Parameter]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000E475 File Offset: 0x0000C675
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x0000E49B File Offset: 0x0000C69B
		[Parameter]
		public SwitchParameter ReadFromDomainController
		{
			get
			{
				return (SwitchParameter)(base.Fields["ReadFromDomainController"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ReadFromDomainController"] = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000E4B3 File Offset: 0x0000C6B3
		protected override Unlimited<uint> DefaultResultSize
		{
			get
			{
				return GetRecipientObjectTask<TIdentity, TDataObject>.defaultResultSize;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000E4BA File Offset: 0x0000C6BA
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000E4BD File Offset: 0x0000C6BD
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000E4E3 File Offset: 0x0000C6E3
		protected SwitchParameter InternalIgnoreDefaultScope
		{
			get
			{
				return (SwitchParameter)(base.Fields["IgnoreDefaultScope"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IgnoreDefaultScope"] = value;
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			if (this.InternalIgnoreDefaultScope && base.DomainController != null)
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorIgnoreDefaultScopeAndDCSetTogether), ErrorCategory.InvalidArgument, null);
			}
			if (this.ResultSize == 0U)
			{
				base.ThrowTerminatingError(new TaskException(Strings.ErrorInvalidResultSize), ErrorCategory.InvalidArgument, null);
			}
			if (this.Credential != null)
			{
				try
				{
					base.NetCredential = this.Credential.GetNetworkCredential();
				}
				catch (PSArgumentException exception)
				{
					base.ThrowTerminatingError(exception, ErrorCategory.InvalidArgument, this.Credential);
				}
			}
			base.InternalBeginProcessing();
			TaskLogger.LogExit();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.NetCredential, base.SessionSettings, 285, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\GetAdObjectTask.cs");
			if (this.InternalIgnoreDefaultScope)
			{
				tenantOrRootOrgRecipientSession.EnforceDefaultScope = false;
				tenantOrRootOrgRecipientSession.UseGlobalCatalog = (this.Identity == null);
			}
			else
			{
				bool flag;
				if (this.ReadFromDomainController && this.Identity != null)
				{
					TIdentity identity = this.Identity;
					flag = ADObjectId.IsValidDistinguishedName(identity.RawIdentity);
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				bool viewEntireForest = base.ServerSettings.ViewEntireForest;
				tenantOrRootOrgRecipientSession.UseGlobalCatalog = (base.DomainController == null && viewEntireForest && !flag2);
			}
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000E66E File Offset: 0x0000C86E
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return true;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000E671 File Offset: 0x0000C871
		protected virtual bool ShouldSkipObject(IConfigurable dataObject)
		{
			return false;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000E674 File Offset: 0x0000C874
		protected virtual bool ShouldSkipPresentationObject(IConfigurable presentationObject)
		{
			return false;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000E678 File Offset: 0x0000C878
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			if (this.Identity != null && this.InternalIgnoreDefaultScope)
			{
				ADObjectId adobjectId;
				if (!RecipientTaskHelper.IsValidDistinguishedName(this.Identity, out adobjectId))
				{
					base.WriteError(new ArgumentException(Strings.ErrorOnlyDNSupportedWithIgnoreDefaultScope), (ErrorCategory)1000, this.Identity);
				}
				IConfigurable dataObject = RecipientTaskHelper.ResolveDataObject<TDataObject>(base.DataSession, null, base.ServerSettings, this.Identity, this.RootId, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<TDataObject>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
				this.WriteResult(dataObject);
			}
			else
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000E768 File Offset: 0x0000C968
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			IDirectorySession directorySession = base.DataSession as IDirectorySession;
			IConfigurable configurable = dataObject;
			if (this.ReadFromDomainController.IsPresent && directorySession != null && directorySession.UseGlobalCatalog)
			{
				try
				{
					directorySession.UseGlobalCatalog = false;
					base.WriteVerbose(Strings.VerboseRereadADObject(dataObject.Identity.ToString(), typeof(TDataObject).Name, ((ADObjectId)dataObject.Identity).ToDNString()));
					configurable = base.DataSession.Read<TDataObject>(dataObject.Identity);
					base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
				}
				finally
				{
					directorySession.UseGlobalCatalog = true;
				}
			}
			ADRecipient adrecipient = configurable as ADRecipient;
			if (adrecipient != null)
			{
				adrecipient.PopulateAcceptMessagesOnlyFromSendersOrMembers();
				adrecipient.PopulateBypassModerationFromSendersOrMembers();
				adrecipient.PopulateRejectMessagesFromSendersOrMembers();
				GetRecipientObjectTask<TIdentity, TDataObject>.SanitizeMailTips(adrecipient);
			}
			if (configurable == null)
			{
				base.WriteVerbose(Strings.VerboseFailedToReadFromDC(dataObject.Identity.ToString(), base.DataSession.Source));
			}
			else if (this.ShouldSkipObject(configurable))
			{
				base.WriteVerbose(Strings.VerboseSkipObject(configurable.Identity.ToString()));
			}
			else
			{
				IConfigurable configurable2 = this.ConvertDataObjectToPresentationObject(configurable);
				if (this.ShouldSkipPresentationObject(configurable2))
				{
					base.WriteVerbose(Strings.VerboseSkipObject(configurable.Identity.ToString()));
				}
				else
				{
					base.WriteResult(configurable2);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000E8BC File Offset: 0x0000CABC
		private static void SanitizeMailTips(ADRecipient recipient)
		{
			if (recipient.MailTipTranslations != null)
			{
				bool isReadOnly = recipient.IsReadOnly;
				if (isReadOnly)
				{
					recipient.SetIsReadOnly(false);
				}
				for (int i = 0; i < recipient.MailTipTranslations.Count; i++)
				{
					string str;
					string text;
					if (ADRecipient.TryGetMailTipParts(recipient.MailTipTranslations[i], out str, out text) && !string.IsNullOrEmpty(text))
					{
						using (StringReader stringReader = new StringReader(text))
						{
							using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
							{
								HtmlToHtml htmlToHtml = new HtmlToHtml();
								htmlToHtml.SetPreserveDisplayNoneStyle(true);
								htmlToHtml.InputEncoding = Encoding.UTF8;
								htmlToHtml.OutputEncoding = Encoding.UTF8;
								htmlToHtml.FilterHtml = true;
								htmlToHtml.Convert(stringReader, stringWriter);
								string str2 = stringWriter.ToString();
								recipient.MailTipTranslations[i] = str + ":" + str2;
							}
						}
					}
				}
				if (isReadOnly)
				{
					recipient.SetIsReadOnly(true);
				}
			}
		}

		// Token: 0x040000F5 RID: 245
		private static readonly Unlimited<uint> defaultResultSize = new Unlimited<uint>(1000U);
	}
}
