using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000292 RID: 658
	[DataContract]
	public class NewAccount : OrgPersonBasicProperties
	{
		// Token: 0x17001D33 RID: 7475
		// (get) Token: 0x06002AD8 RID: 10968 RVA: 0x0008605F File Offset: 0x0008425F
		public override string AssociatedCmdlet
		{
			get
			{
				if (this.associatedCmdlet != null)
				{
					return this.associatedCmdlet;
				}
				return "New-Mailbox";
			}
		}

		// Token: 0x17001D34 RID: 7476
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x00086075 File Offset: 0x00084275
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x17001D35 RID: 7477
		// (get) Token: 0x06002ADA RID: 10970 RVA: 0x0008607C File Offset: 0x0008427C
		// (set) Token: 0x06002ADB RID: 10971 RVA: 0x00086084 File Offset: 0x00084284
		public SetCalendarProcessing SetCalendarProcessing { get; private set; }

		// Token: 0x17001D36 RID: 7478
		// (get) Token: 0x06002ADC RID: 10972 RVA: 0x0008608D File Offset: 0x0008428D
		// (set) Token: 0x06002ADD RID: 10973 RVA: 0x000860A9 File Offset: 0x000842A9
		[DataMember]
		public bool ResetPasswordOnNextLogon
		{
			get
			{
				return (bool)(base[UserSchema.ResetPasswordOnNextLogon] ?? false);
			}
			set
			{
				base[UserSchema.ResetPasswordOnNextLogon] = value;
			}
		}

		// Token: 0x17001D37 RID: 7479
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x000860BC File Offset: 0x000842BC
		// (set) Token: 0x06002ADF RID: 10975 RVA: 0x000860C4 File Offset: 0x000842C4
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x17001D38 RID: 7480
		// (get) Token: 0x06002AE0 RID: 10976 RVA: 0x000860CD File Offset: 0x000842CD
		// (set) Token: 0x06002AE1 RID: 10977 RVA: 0x000860D5 File Offset: 0x000842D5
		[DataMember]
		public string DomainName { get; set; }

		// Token: 0x17001D39 RID: 7481
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x000860DE File Offset: 0x000842DE
		// (set) Token: 0x06002AE3 RID: 10979 RVA: 0x000860E6 File Offset: 0x000842E6
		[DataMember]
		public string Password { private get; set; }

		// Token: 0x17001D3A RID: 7482
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x000860EF File Offset: 0x000842EF
		// (set) Token: 0x06002AE5 RID: 10981 RVA: 0x00086101 File Offset: 0x00084301
		public string UserPrincipalName
		{
			get
			{
				return (string)base[MailboxSchema.UserPrincipalName];
			}
			set
			{
				base[MailboxSchema.UserPrincipalName] = value;
			}
		}

		// Token: 0x17001D3B RID: 7483
		// (get) Token: 0x06002AE6 RID: 10982 RVA: 0x0008610F File Offset: 0x0008430F
		// (set) Token: 0x06002AE7 RID: 10983 RVA: 0x00086121 File Offset: 0x00084321
		public string WindowsLiveID
		{
			get
			{
				return (string)base[MailboxSchema.WindowsLiveID];
			}
			set
			{
				base[MailboxSchema.WindowsLiveID] = value;
			}
		}

		// Token: 0x17001D3C RID: 7484
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x0008612F File Offset: 0x0008432F
		// (set) Token: 0x06002AE9 RID: 10985 RVA: 0x00086141 File Offset: 0x00084341
		public string MicrosoftOnlineServicesID
		{
			get
			{
				return (string)base["MicrosoftOnlineServicesID"];
			}
			set
			{
				base["MicrosoftOnlineServicesID"] = value;
			}
		}

		// Token: 0x17001D3D RID: 7485
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x0008614F File Offset: 0x0008434F
		// (set) Token: 0x06002AEB RID: 10987 RVA: 0x00086161 File Offset: 0x00084361
		public string PrimarySmtpAddress
		{
			get
			{
				return (string)base[MailEnabledRecipientSchema.PrimarySmtpAddress];
			}
			set
			{
				base[MailEnabledRecipientSchema.PrimarySmtpAddress] = value;
			}
		}

		// Token: 0x17001D3E RID: 7486
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x0008616F File Offset: 0x0008436F
		// (set) Token: 0x06002AED RID: 10989 RVA: 0x00086181 File Offset: 0x00084381
		public string Name
		{
			get
			{
				return (string)base[ADObjectSchema.Name];
			}
			set
			{
				base[ADObjectSchema.Name] = value;
			}
		}

		// Token: 0x17001D3F RID: 7487
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x0008618F File Offset: 0x0008438F
		// (set) Token: 0x06002AEF RID: 10991 RVA: 0x000861A1 File Offset: 0x000843A1
		public bool? UseExistingLiveId
		{
			get
			{
				return (bool?)base["UseExistingLiveId"];
			}
			set
			{
				base["UseExistingLiveId"] = value;
			}
		}

		// Token: 0x17001D40 RID: 7488
		// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x000861B4 File Offset: 0x000843B4
		// (set) Token: 0x06002AF1 RID: 10993 RVA: 0x000861C6 File Offset: 0x000843C6
		public ADObjectId Organization
		{
			get
			{
				return (ADObjectId)base["Organization"];
			}
			set
			{
				base["Organization"] = value;
			}
		}

		// Token: 0x17001D41 RID: 7489
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000861D4 File Offset: 0x000843D4
		// (set) Token: 0x06002AF3 RID: 10995 RVA: 0x000861E6 File Offset: 0x000843E6
		[DataMember]
		public string MailboxPlan
		{
			get
			{
				return (string)base[MailboxSchema.MailboxPlan];
			}
			set
			{
				base[MailboxSchema.MailboxPlan] = value;
			}
		}

		// Token: 0x17001D42 RID: 7490
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x000861F4 File Offset: 0x000843F4
		// (set) Token: 0x06002AF5 RID: 10997 RVA: 0x00086206 File Offset: 0x00084406
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

		// Token: 0x17001D43 RID: 7491
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x00086214 File Offset: 0x00084414
		// (set) Token: 0x06002AF7 RID: 10999 RVA: 0x00086226 File Offset: 0x00084426
		[DataMember]
		public bool? Room
		{
			get
			{
				return (bool?)base["Room"];
			}
			set
			{
				base["Room"] = value;
			}
		}

		// Token: 0x17001D44 RID: 7492
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x00086239 File Offset: 0x00084439
		// (set) Token: 0x06002AF9 RID: 11001 RVA: 0x0008624B File Offset: 0x0008444B
		[DataMember]
		public string Office
		{
			get
			{
				return (string)base[MailboxSchema.Office];
			}
			set
			{
				base[MailboxSchema.Office] = value;
			}
		}

		// Token: 0x17001D45 RID: 7493
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x00086259 File Offset: 0x00084459
		// (set) Token: 0x06002AFB RID: 11003 RVA: 0x0008626B File Offset: 0x0008446B
		[DataMember]
		public string Phone
		{
			get
			{
				return (string)base[ADUserSchema.Phone];
			}
			set
			{
				base[ADUserSchema.Phone] = value;
			}
		}

		// Token: 0x17001D46 RID: 7494
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x00086279 File Offset: 0x00084479
		// (set) Token: 0x06002AFD RID: 11005 RVA: 0x0008628B File Offset: 0x0008448B
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

		// Token: 0x17001D47 RID: 7495
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x00086299 File Offset: 0x00084499
		// (set) Token: 0x06002AFF RID: 11007 RVA: 0x000862A6 File Offset: 0x000844A6
		[DataMember]
		public string AutomaticBooking
		{
			get
			{
				return this.SetCalendarProcessing.AutomaticBooking;
			}
			set
			{
				this.SetCalendarProcessing.AutomaticBooking = value;
			}
		}

		// Token: 0x17001D48 RID: 7496
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x000862B4 File Offset: 0x000844B4
		// (set) Token: 0x06002B01 RID: 11009 RVA: 0x000862C1 File Offset: 0x000844C1
		[DataMember]
		public Identity[] ResourceDelegates
		{
			get
			{
				return this.SetCalendarProcessing.ResourceDelegates;
			}
			set
			{
				this.SetCalendarProcessing.ResourceDelegates = value;
			}
		}

		// Token: 0x17001D49 RID: 7497
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x000862CF File Offset: 0x000844CF
		// (set) Token: 0x06002B03 RID: 11011 RVA: 0x000862E1 File Offset: 0x000844E1
		[DataMember]
		public bool? ImportLiveId
		{
			get
			{
				return (bool?)base["ImportLiveId"];
			}
			set
			{
				base["ImportLiveId"] = value;
			}
		}

		// Token: 0x17001D4A RID: 7498
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x000862F4 File Offset: 0x000844F4
		// (set) Token: 0x06002B05 RID: 11013 RVA: 0x00086306 File Offset: 0x00084506
		[DataMember]
		public bool EvictLiveId
		{
			get
			{
				return (bool)base["EvictLiveId"];
			}
			set
			{
				base["EvictLiveId"] = value;
			}
		}

		// Token: 0x17001D4B RID: 7499
		// (get) Token: 0x06002B06 RID: 11014 RVA: 0x00086319 File Offset: 0x00084519
		// (set) Token: 0x06002B07 RID: 11015 RVA: 0x00086321 File Offset: 0x00084521
		[DataMember]
		public string RemovedMailbox { get; set; }

		// Token: 0x17001D4C RID: 7500
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x0008632A File Offset: 0x0008452A
		// (set) Token: 0x06002B09 RID: 11017 RVA: 0x00086332 File Offset: 0x00084532
		[DataMember]
		public string OriginalLiveID { get; set; }

		// Token: 0x17001D4D RID: 7501
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x0008633B File Offset: 0x0008453B
		// (set) Token: 0x06002B0B RID: 11019 RVA: 0x00086343 File Offset: 0x00084543
		[DataMember]
		public bool IsPasswordRequired { get; set; }

		// Token: 0x17001D4E RID: 7502
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x0008634C File Offset: 0x0008454C
		// (set) Token: 0x06002B0D RID: 11021 RVA: 0x00086354 File Offset: 0x00084554
		[DataMember]
		public string SoftDeletedMailbox { get; set; }

		// Token: 0x06002B0E RID: 11022 RVA: 0x0008635D File Offset: 0x0008455D
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.SetCalendarProcessing = new SetCalendarProcessing();
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x0008636C File Offset: 0x0008456C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (string.IsNullOrEmpty(this.SoftDeletedMailbox))
			{
				base.DisplayName.FaultIfNullOrEmpty(OwaOptionStrings.DisplayNameNotSetError);
			}
			this.Name = ((!string.IsNullOrEmpty(base.DisplayName) && base.DisplayName.Length >= 64) ? base.DisplayName.SurrogateSubstring(0, 64) : base.DisplayName);
			this.UserName.FaultIfNullOrEmpty(OwaOptionStrings.UserNameNotSetError);
			this.DomainName.FaultIfNullOrEmpty(OwaOptionStrings.DomainNameNotSetError);
			string text = this.UserName + "@" + this.DomainName;
			if (!string.IsNullOrEmpty(this.SoftDeletedMailbox))
			{
				this.associatedCmdlet = "Undo-SoftDeletedMailbox";
				base["SoftDeletedObject"] = this.SoftDeletedMailbox;
				if (RbacPrincipal.Current.IsInRole("LiveID"))
				{
					this.WindowsLiveID = text;
				}
				if (!string.IsNullOrEmpty(this.Password))
				{
					base["Password"] = this.Password.ToSecureString();
					return;
				}
			}
			else
			{
				if (this.Room == true)
				{
					this.PrimarySmtpAddress = text;
				}
				else if (RbacPrincipal.Current.IsInRole("MultiTenant+New-Mailbox?DisplayName&Password&Name&MicrosoftOnlineServicesID@W:Organization"))
				{
					this.MicrosoftOnlineServicesID = text;
				}
				else if (RbacPrincipal.Current.IsInRole("LiveID"))
				{
					this.WindowsLiveID = text;
				}
				else
				{
					this.UserPrincipalName = text;
				}
				if (!this.IsNameUnique())
				{
					string text2 = string.Format(" {0}", Guid.NewGuid().ToString("B").ToUpperInvariant());
					int num = 64 - text2.Length;
					if (this.Name.Length > num)
					{
						this.Name = this.Name.SurrogateSubstring(0, num);
					}
					this.Name += text2;
				}
				if (!string.IsNullOrEmpty(this.RemovedMailbox))
				{
					base["RemovedMailbox"] = this.RemovedMailbox;
					if (!this.IsPasswordRequired && !string.IsNullOrEmpty(this.WindowsLiveID) && this.WindowsLiveID == this.OriginalLiveID)
					{
						this.UseExistingLiveId = new bool?(true);
					}
				}
				if (this.Room != true && this.ImportLiveId != true && this.UseExistingLiveId != true)
				{
					base["Password"] = this.Password.ToSecureString();
				}
			}
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x0008660C File Offset: 0x0008480C
		private bool IsNameUnique()
		{
			IRecipientSession recipientSession = (IRecipientSession)((RecipientObjectResolver)RecipientObjectResolver.Instance).CreateAdSession();
			recipientSession.UseConfigNC = false;
			recipientSession.UseGlobalCatalog = true;
			recipientSession.EnforceDefaultScope = false;
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, this.Name);
			ADRecipient[] array = recipientSession.Find(null, QueryScope.SubTree, filter, null, 1);
			return array.Length <= 0;
		}

		// Token: 0x0400216B RID: 8555
		public const string RbacParameters_NonLiveID = "?DisplayName&Password&Name&UserPrincipalName";

		// Token: 0x0400216C RID: 8556
		public const string RbacParameters_WLID = "?DisplayName&Password&Name&WindowsLiveID";

		// Token: 0x0400216D RID: 8557
		public const string RbacParameters_MOSID = "?DisplayName&Password&Name&MicrosoftOnlineServicesID";

		// Token: 0x0400216E RID: 8558
		public const string RbacParameters_MultiTenant_Room = "?DisplayName&Name&Room&PrimarySmtpAddress";

		// Token: 0x0400216F RID: 8559
		private const string RbacParameters = "?DisplayName&Password&Name";

		// Token: 0x04002170 RID: 8560
		private string associatedCmdlet;
	}
}
