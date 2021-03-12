using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200028E RID: 654
	[DataContract]
	public class MailboxFilter : RecipientFilter
	{
		// Token: 0x17001D12 RID: 7442
		// (get) Token: 0x06002A95 RID: 10901 RVA: 0x00085A27 File Offset: 0x00083C27
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x17001D13 RID: 7443
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x00085A30 File Offset: 0x00083C30
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return new RecipientTypeDetails[]
				{
					RecipientTypeDetails.UserMailbox,
					RecipientTypeDetails.LinkedMailbox,
					RecipientTypeDetails.SharedMailbox,
					RecipientTypeDetails.TeamMailbox,
					RecipientTypeDetails.LegacyMailbox,
					RecipientTypeDetails.RoomMailbox,
					RecipientTypeDetails.EquipmentMailbox
				};
			}
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x00085A74 File Offset: 0x00083C74
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			try
			{
				switch (string.IsNullOrEmpty(base.SelectedView) ? MailboxView.All : ((MailboxView)Enum.Parse(typeof(MailboxView), base.SelectedView)))
				{
				case MailboxView.User:
					base.RecipientTypeDetailsList = new RecipientTypeDetails[]
					{
						RecipientTypeDetails.UserMailbox
					};
					break;
				case MailboxView.ManagedUser:
					base.RecipientTypeDetailsList = new RecipientTypeDetails[]
					{
						RecipientTypeDetails.UserMailbox
					};
					base["AuthenticationType"] = AuthenticationType.Managed;
					break;
				case MailboxView.FederatedUser:
					base.RecipientTypeDetailsList = new RecipientTypeDetails[]
					{
						RecipientTypeDetails.UserMailbox
					};
					base["AuthenticationType"] = AuthenticationType.Federated;
					break;
				case MailboxView.RoomMailbox:
					base.RecipientTypeDetailsList = new RecipientTypeDetails[]
					{
						RecipientTypeDetails.RoomMailbox
					};
					break;
				case MailboxView.LitigationHold:
					base["Filter"] = "LitigationHoldEnabled -eq $true";
					break;
				}
			}
			catch (ArgumentException)
			{
			}
		}
	}
}
