using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MessagingPolicies.AddressRewrite
{
	// Token: 0x02000007 RID: 7
	[Cmdlet("new", "AddressRewriteEntry", SupportsShouldProcess = true)]
	public class NewAddressRewriteEntry : NewSystemConfigurationObjectTask<AddressRewriteEntry>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002EF7 File Offset: 0x000010F7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewAddressrewriteentry(base.Name.ToString(), this.InternalAddress.ToString(), this.ExternalAddress.ToString());
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002F1F File Offset: 0x0000111F
		public NewAddressRewriteEntry()
		{
			this.OutboundOnly = false;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002F2E File Offset: 0x0000112E
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002F45 File Offset: 0x00001145
		[Parameter(Mandatory = true)]
		public string InternalAddress
		{
			get
			{
				return (string)base.Fields["InternalAddress"];
			}
			set
			{
				base.Fields["InternalAddress"] = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002F58 File Offset: 0x00001158
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002F6F File Offset: 0x0000116F
		[Parameter(Mandatory = true)]
		public string ExternalAddress
		{
			get
			{
				return (string)base.Fields["ExternalAddress"];
			}
			set
			{
				base.Fields["ExternalAddress"] = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002F82 File Offset: 0x00001182
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002F99 File Offset: 0x00001199
		[Parameter(Mandatory = false)]
		public bool OutboundOnly
		{
			get
			{
				return (bool)base.Fields["OutboundOnly"];
			}
			set
			{
				base.Fields["OutboundOnly"] = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002FB1 File Offset: 0x000011B1
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002FC8 File Offset: 0x000011C8
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExceptionList
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["ExceptionList"];
			}
			set
			{
				base.Fields["ExceptionList"] = value;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002FDC File Offset: 0x000011DC
		protected override IConfigurable PrepareDataObject()
		{
			AddressRewriteEntry addressRewriteEntry = (AddressRewriteEntry)base.PrepareDataObject();
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			configurationSession.UseConfigNC = false;
			ADObjectId adobjectId;
			try
			{
				adobjectId = Utils.ValidateEntryAddresses(this.InternalAddress, this.ExternalAddress, this.OutboundOnly, this.ExceptionList, configurationSession, null);
			}
			catch (ArgumentException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
				return null;
			}
			catch (FormatException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
				return null;
			}
			addressRewriteEntry.InternalAddress = this.InternalAddress;
			addressRewriteEntry.ExternalAddress = this.ExternalAddress;
			addressRewriteEntry.OutboundOnly = this.OutboundOnly;
			addressRewriteEntry.ExceptionList = this.ExceptionList;
			ADObjectId childId = adobjectId.GetChildId(base.Name);
			addressRewriteEntry.SetId(childId);
			return addressRewriteEntry;
		}
	}
}
