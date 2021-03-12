using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MessagingPolicies.AddressRewrite
{
	// Token: 0x02000009 RID: 9
	[Cmdlet("set", "addressrewriteentry", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class SetAddressRewriteEntry : SetSystemConfigurationObjectTask<AddressRewriteEntryIdParameter, AddressRewriteEntry>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000030FE File Offset: 0x000012FE
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAddressrewriteentry(this.Identity.ToString());
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003110 File Offset: 0x00001310
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.CreateSession();
			configurationSession.UseConfigNC = false;
			return configurationSession;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003131 File Offset: 0x00001331
		protected override ObjectId RootId
		{
			get
			{
				return Utils.RootId;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003138 File Offset: 0x00001338
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalValidate();
				AddressRewriteEntry dataObject = this.DataObject;
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				configurationSession.UseConfigNC = false;
				Utils.ValidateEntryAddresses(dataObject.InternalAddress, dataObject.ExternalAddress, dataObject.OutboundOnly, dataObject.ExceptionList, configurationSession, new Guid?(dataObject.Guid));
			}
			catch (ArgumentException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
			catch (FormatException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}
	}
}
