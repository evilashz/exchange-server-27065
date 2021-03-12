using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007DD RID: 2013
	[Cmdlet("Remove", "AddressList", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveAddressList : RemoveAddressBookBase<AddressListIdParameter>
	{
		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06004686 RID: 18054 RVA: 0x001210FC File Offset: 0x0011F2FC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.Recursive)
				{
					return Strings.ConfirmationMessageRemoveAddressListRecursively(this.Identity.ToString());
				}
				return Strings.ConfirmationMessageRemoveAddressList(this.Identity.ToString());
			}
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06004687 RID: 18055 RVA: 0x0012112C File Offset: 0x0011F32C
		// (set) Token: 0x06004688 RID: 18056 RVA: 0x00121152 File Offset: 0x0011F352
		[Parameter]
		public SwitchParameter Recursive
		{
			get
			{
				return (SwitchParameter)(base.Fields["Recursive"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Recursive"] = value;
			}
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x0012116A File Offset: 0x0011F36A
		private void MoveToNextPercent()
		{
			this.currentProcent = 5 + this.currentProcent % 95;
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x00121180 File Offset: 0x0011F380
		private void ReportDeleteTreeProgress(ADTreeDeleteNotFinishedException de)
		{
			this.MoveToNextPercent();
			if (de != null)
			{
				base.WriteVerbose(de.LocalizedString);
			}
			else
			{
				base.WriteVerbose(Strings.ProgressStatusRemovingAddressListTree);
			}
			base.WriteProgress(Strings.ProgressActivityRemovingAddressListTree(base.DataObject.Id.ToString()), Strings.ProgressStatusRemovingAddressListTree, this.currentProcent);
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x001211D5 File Offset: 0x0011F3D5
		protected override bool HandleRemoveWithAssociatedAddressBookPolicies()
		{
			base.WriteError(new InvalidOperationException(Strings.ErrorRemoveAddressListWithAssociatedAddressBookPolicies(base.DataObject.Name)), ErrorCategory.InvalidOperation, base.DataObject.Identity);
			return false;
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x00121204 File Offset: 0x0011F404
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.Recursive)
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				List<ADObjectId> list = new List<ADObjectId>();
				try
				{
					this.currentProcent = 0;
					ADPagedReader<AddressBookBase> adpagedReader = configurationSession.FindPaged<AddressBookBase>(base.DataObject.Id, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Id, base.DataObject.Id), null, 0);
					foreach (AddressBookBase addressBookBase in adpagedReader)
					{
						list.Add(addressBookBase.Id);
						if (list.Count % ADGenericPagedReader<AddressBookBase>.DefaultPageSize == 0)
						{
							this.MoveToNextPercent();
							base.WriteProgress(Strings.ProgressActivityRemovingAddressListTree(base.DataObject.Id.ToString()), Strings.ProgressStatusReadingAddressListTree(list.Count), this.currentProcent);
						}
					}
					list.Add(base.DataObject.Id);
					if (this.currentProcent != 0)
					{
						this.ReportDeleteTreeProgress(null);
					}
					configurationSession.DeleteTree(base.DataObject, new TreeDeleteNotFinishedHandler(this.ReportDeleteTreeProgress));
				}
				catch (DataSourceTransientException exception)
				{
					base.WriteError(exception, ErrorCategory.WriteError, base.DataObject.Identity);
					TaskLogger.LogExit();
					return;
				}
				try
				{
					UpdateAddressBookBase<AddressListIdParameter>.UpdateRecipients(base.DataObject, list.ToArray(), base.DomainController, base.TenantGlobalCatalogSession, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new WriteProgress(base.WriteProgress), this);
					goto IL_206;
				}
				catch (DataSourceTransientException ex)
				{
					this.WriteWarning(Strings.ErrorReadMatchingRecipients(this.Identity.ToString(), base.DataObject.LdapRecipientFilter, ex.Message));
					TaskLogger.Trace("Exception is raised while reading recipients: {0}", new object[]
					{
						ex.ToString()
					});
					goto IL_206;
				}
				catch (DataSourceOperationException ex2)
				{
					this.WriteWarning(Strings.ErrorReadMatchingRecipients(this.Identity.ToString(), base.DataObject.LdapRecipientFilter, ex2.Message));
					TaskLogger.Trace("Exception is raised while reading recipients matching filter: {0}", new object[]
					{
						ex2.ToString()
					});
					goto IL_206;
				}
			}
			base.InternalProcessRecord();
			IL_206:
			TaskLogger.LogExit();
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x00121450 File Offset: 0x0011F650
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return AddressList.FromDataObject((AddressBookBase)dataObject);
		}

		// Token: 0x04002B03 RID: 11011
		private int currentProcent;
	}
}
