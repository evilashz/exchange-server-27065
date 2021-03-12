using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Net.AAD;
using Microsoft.Exchange.UnifiedGroups;
using Microsoft.WindowsAzure.ActiveDirectory;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200001C RID: 28
	[Cmdlet("Get", "UnifiedGroup")]
	public sealed class GetUnifiedGroup : UnifiedGroupTask
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006264 File Offset: 0x00004464
		// (set) Token: 0x0600013D RID: 317 RVA: 0x0000626C File Offset: 0x0000446C
		[Parameter(Mandatory = false)]
		public Guid? Identity { get; set; }

		// Token: 0x0600013E RID: 318 RVA: 0x00006278 File Offset: 0x00004478
		protected override void InternalProcessRecord()
		{
			AADClient aadclient = AADClientFactory.Create(base.OrganizationId, GraphProxyVersions.Version14);
			if (aadclient == null)
			{
				base.WriteError(new TaskException(Strings.ErrorUnableToSessionWithAAD), ExchangeErrorCategory.Client, null);
				return;
			}
			if (this.Identity != null)
			{
				Group group;
				try
				{
					group = aadclient.GetGroup(this.Identity.Value.ToString(), true);
				}
				catch (AADException ex)
				{
					base.WriteVerbose("GetGroup failed with exception: {0}", new object[]
					{
						ex
					});
					base.WriteError(new TaskException(Strings.ErrorUnableToGetUnifiedGroup), base.GetErrorCategory(ex), null);
					return;
				}
				aadclient.Service.LoadProperty(group, "createdOnBehalfOf");
				aadclient.Service.LoadProperty(group, "members");
				aadclient.Service.LoadProperty(group, "owners");
				this.WriteAADGroupObject(group);
				return;
			}
			try
			{
				foreach (Group group2 in aadclient.GetGroups())
				{
					this.WriteAADGroupObject(group2);
				}
			}
			catch (AADException ex2)
			{
				base.WriteVerbose("GetGroups failed with exception: {0}", new object[]
				{
					ex2
				});
				base.WriteError(new TaskException(Strings.ErrorUnableToGetUnifiedGroup), base.GetErrorCategory(ex2), null);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000063F8 File Offset: 0x000045F8
		private void WriteAADGroupObject(Group group)
		{
			AADGroupPresentationObject aadgroupPresentationObject = new AADGroupPresentationObject(group);
			if (base.NeedSuppressingPiiData)
			{
				aadgroupPresentationObject = this.Redact(aadgroupPresentationObject);
			}
			base.WriteObject(aadgroupPresentationObject);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006424 File Offset: 0x00004624
		private AADDirectoryObjectPresentationObject[] Redact(AADDirectoryObjectPresentationObject[] values)
		{
			if (values != null && values.Length > 0)
			{
				for (int i = 0; i < values.Length; i++)
				{
					values[i] = this.Redact(values[i]);
				}
			}
			return values;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006458 File Offset: 0x00004658
		private AADDirectoryObjectPresentationObject Redact(AADDirectoryObjectPresentationObject value)
		{
			if (value != null)
			{
				AADUserPresentationObject aaduserPresentationObject = value as AADUserPresentationObject;
				if (aaduserPresentationObject != null)
				{
					return this.Redact(aaduserPresentationObject);
				}
				AADGroupPresentationObject aadgroupPresentationObject = value as AADGroupPresentationObject;
				if (aadgroupPresentationObject != null)
				{
					return this.Redact(aadgroupPresentationObject);
				}
				value.Owners = this.Redact(value.Owners);
				value.Members = this.Redact(value.Members);
			}
			return value;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000064B4 File Offset: 0x000046B4
		private AADUserPresentationObject Redact(AADUserPresentationObject value)
		{
			if (value != null)
			{
				value.DisplayName = SuppressingPiiData.Redact(value.DisplayName);
				value.MailNickname = SuppressingPiiData.Redact(value.MailNickname);
				value.Owners = this.Redact(value.Owners);
				value.Members = this.Redact(value.Members);
			}
			return value;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000650C File Offset: 0x0000470C
		private AADGroupPresentationObject Redact(AADGroupPresentationObject value)
		{
			if (value != null)
			{
				value.AllowAccessTo = this.Redact(value.AllowAccessTo);
				value.Description = SuppressingPiiData.Redact(value.Description);
				value.DisplayName = SuppressingPiiData.Redact(value.DisplayName);
				value.ExchangeResources = SuppressingPiiData.Redact(value.ExchangeResources);
				value.Mail = SuppressingPiiData.Redact(value.Mail);
				value.PendingMembers = this.Redact(value.PendingMembers);
				value.ProxyAddresses = SuppressingPiiData.Redact(value.ProxyAddresses);
				value.SharePointResources = SuppressingPiiData.Redact(value.SharePointResources);
				value.Owners = this.Redact(value.Owners);
				value.Members = this.Redact(value.Members);
			}
			return value;
		}
	}
}
