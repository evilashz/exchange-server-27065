using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000293 RID: 659
	public class SetMailbox : SetObjectProperties
	{
		// Token: 0x17001D4F RID: 7503
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x00086672 File Offset: 0x00084872
		// (set) Token: 0x06002B13 RID: 11027 RVA: 0x0008667A File Offset: 0x0008487A
		public IEnumerable<string> EmailAddresses { get; set; }

		// Token: 0x17001D50 RID: 7504
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x00086683 File Offset: 0x00084883
		// (set) Token: 0x06002B15 RID: 11029 RVA: 0x00086695 File Offset: 0x00084895
		[DataMember]
		public string MailTip
		{
			get
			{
				return (string)base["MailTip"];
			}
			set
			{
				base["MailTip"] = value;
			}
		}

		// Token: 0x17001D51 RID: 7505
		// (get) Token: 0x06002B16 RID: 11030 RVA: 0x000866A3 File Offset: 0x000848A3
		// (set) Token: 0x06002B17 RID: 11031 RVA: 0x000866B5 File Offset: 0x000848B5
		[DataMember]
		public string MailboxPlan
		{
			get
			{
				return (string)base[ADRecipientSchema.MailboxPlan];
			}
			set
			{
				base[ADRecipientSchema.MailboxPlan] = value;
			}
		}

		// Token: 0x17001D52 RID: 7506
		// (get) Token: 0x06002B18 RID: 11032 RVA: 0x000866C3 File Offset: 0x000848C3
		// (set) Token: 0x06002B19 RID: 11033 RVA: 0x000866D5 File Offset: 0x000848D5
		[DataMember]
		public string RoleAssignmentPolicy
		{
			get
			{
				return (string)base[MailboxSchema.RoleAssignmentPolicy];
			}
			set
			{
				base[MailboxSchema.RoleAssignmentPolicy] = value;
			}
		}

		// Token: 0x17001D53 RID: 7507
		// (get) Token: 0x06002B1A RID: 11034 RVA: 0x000866E3 File Offset: 0x000848E3
		// (set) Token: 0x06002B1B RID: 11035 RVA: 0x000866F5 File Offset: 0x000848F5
		[DataMember]
		public string RetentionPolicy
		{
			get
			{
				return (string)base[MailboxSchema.RetentionPolicy];
			}
			set
			{
				base[MailboxSchema.RetentionPolicy] = value;
			}
		}

		// Token: 0x17001D54 RID: 7508
		// (get) Token: 0x06002B1C RID: 11036 RVA: 0x00086703 File Offset: 0x00084903
		// (set) Token: 0x06002B1D RID: 11037 RVA: 0x00086715 File Offset: 0x00084915
		[DataMember]
		public string ResourceCapacity
		{
			get
			{
				return (string)base[MailboxSchema.ResourceCapacity];
			}
			set
			{
				base[MailboxSchema.ResourceCapacity] = value;
			}
		}

		// Token: 0x17001D55 RID: 7509
		// (get) Token: 0x06002B1E RID: 11038 RVA: 0x00086724 File Offset: 0x00084924
		// (set) Token: 0x06002B1F RID: 11039 RVA: 0x0008675D File Offset: 0x0008495D
		[DataMember]
		public bool? LitigationHoldEnabled
		{
			get
			{
				if (base[MailboxSchema.LitigationHoldEnabled] != null)
				{
					return new bool?((bool)base[MailboxSchema.LitigationHoldEnabled]);
				}
				return null;
			}
			set
			{
				base[MailboxSchema.LitigationHoldEnabled] = value;
			}
		}

		// Token: 0x17001D56 RID: 7510
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x00086770 File Offset: 0x00084970
		// (set) Token: 0x06002B21 RID: 11041 RVA: 0x00086782 File Offset: 0x00084982
		[DataMember]
		public string RetentionComment
		{
			get
			{
				return (string)base[MailboxSchema.RetentionComment];
			}
			set
			{
				base[MailboxSchema.RetentionComment] = value;
			}
		}

		// Token: 0x17001D57 RID: 7511
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x00086790 File Offset: 0x00084990
		// (set) Token: 0x06002B23 RID: 11043 RVA: 0x000867A2 File Offset: 0x000849A2
		[DataMember]
		public string RetentionUrl
		{
			get
			{
				return (string)base[MailboxSchema.RetentionUrl];
			}
			set
			{
				base[MailboxSchema.RetentionUrl] = value;
			}
		}

		// Token: 0x17001D58 RID: 7512
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x000867B0 File Offset: 0x000849B0
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-Mailbox";
			}
		}

		// Token: 0x17001D59 RID: 7513
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x000867B7 File Offset: 0x000849B7
		public override string RbacScope
		{
			get
			{
				return "@W:Self|Organization";
			}
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000867C0 File Offset: 0x000849C0
		public void UpdateEmailAddresses(Mailbox mailbox)
		{
			ProxyAddressCollection emailAddresses = mailbox.EmailAddresses;
			for (int i = emailAddresses.Count - 1; i >= 0; i--)
			{
				if (emailAddresses[i] is SmtpProxyAddress && !((SmtpProxyAddress)emailAddresses[i]).IsPrimaryAddress)
				{
					emailAddresses.RemoveAt(i);
				}
			}
			if (this.EmailAddresses != null)
			{
				foreach (string text in this.EmailAddresses)
				{
					ProxyAddress proxyAddress = ProxyAddress.Parse(text);
					if (proxyAddress is InvalidProxyAddress)
					{
						InvalidProxyAddress invalidProxyAddress = proxyAddress as InvalidProxyAddress;
						throw new FaultException(invalidProxyAddress.ParseException.Message);
					}
					if (emailAddresses.Contains(proxyAddress))
					{
						throw new FaultException(string.Format(OwaOptionStrings.DuplicateProxyAddressError, text));
					}
					emailAddresses.Add(proxyAddress);
				}
			}
			base[MailEnabledRecipientSchema.EmailAddresses] = emailAddresses;
		}
	}
}
