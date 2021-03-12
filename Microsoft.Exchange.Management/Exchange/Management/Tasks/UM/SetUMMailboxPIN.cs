using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D3C RID: 3388
	[Cmdlet("Set", "UMMailboxPIN", SupportsShouldProcess = true)]
	public class SetUMMailboxPIN : UMMailboxTask<MailboxIdParameter>
	{
		// Token: 0x060081D5 RID: 33237 RVA: 0x00212E34 File Offset: 0x00211034
		public SetUMMailboxPIN()
		{
			this.PinExpired = true;
			this.LockedOut = false;
		}

		// Token: 0x17002852 RID: 10322
		// (get) Token: 0x060081D6 RID: 33238 RVA: 0x00212E4A File Offset: 0x0021104A
		// (set) Token: 0x060081D7 RID: 33239 RVA: 0x00212E61 File Offset: 0x00211061
		[LocDescription(Strings.IDs.Pin)]
		[Parameter(Mandatory = false)]
		public string Pin
		{
			get
			{
				return (string)base.Fields["Pin"];
			}
			set
			{
				base.Fields["Pin"] = value;
			}
		}

		// Token: 0x17002853 RID: 10323
		// (get) Token: 0x060081D8 RID: 33240 RVA: 0x00212E74 File Offset: 0x00211074
		// (set) Token: 0x060081D9 RID: 33241 RVA: 0x00212E8B File Offset: 0x0021108B
		[LocDescription(Strings.IDs.PinExpired)]
		[Parameter(Mandatory = false)]
		public bool PinExpired
		{
			get
			{
				return (bool)base.Fields["PinExpired"];
			}
			set
			{
				base.Fields["PinExpired"] = value;
			}
		}

		// Token: 0x17002854 RID: 10324
		// (get) Token: 0x060081DA RID: 33242 RVA: 0x00212EA3 File Offset: 0x002110A3
		// (set) Token: 0x060081DB RID: 33243 RVA: 0x00212EBA File Offset: 0x002110BA
		[LocDescription(Strings.IDs.LockedOut)]
		[Parameter(Mandatory = false)]
		public bool LockedOut
		{
			get
			{
				return (bool)base.Fields["LockedOut"];
			}
			set
			{
				base.Fields["LockedOut"] = value;
			}
		}

		// Token: 0x17002855 RID: 10325
		// (get) Token: 0x060081DC RID: 33244 RVA: 0x00212ED2 File Offset: 0x002110D2
		// (set) Token: 0x060081DD RID: 33245 RVA: 0x00212EE9 File Offset: 0x002110E9
		[Parameter(Mandatory = false)]
		[LocDescription(Strings.IDs.NotifyEmail)]
		public string NotifyEmail
		{
			get
			{
				return (string)base.Fields["NotifyEmail"];
			}
			set
			{
				base.Fields["NotifyEmail"] = value;
			}
		}

		// Token: 0x17002856 RID: 10326
		// (get) Token: 0x060081DE RID: 33246 RVA: 0x00212EFC File Offset: 0x002110FC
		// (set) Token: 0x060081DF RID: 33247 RVA: 0x00212F1D File Offset: 0x0021111D
		[LocDescription(Strings.IDs.SendEmail)]
		[Parameter(Mandatory = false)]
		public bool SendEmail
		{
			get
			{
				return (bool)(base.Fields["SendEmail"] ?? true);
			}
			set
			{
				base.Fields["SendEmail"] = value;
			}
		}

		// Token: 0x17002857 RID: 10327
		// (get) Token: 0x060081E0 RID: 33248 RVA: 0x00212F35 File Offset: 0x00211135
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetUMMailboxPIN(this.Identity.ToString());
			}
		}

		// Token: 0x060081E1 RID: 33249 RVA: 0x00212F48 File Offset: 0x00211148
		protected override void DoValidate()
		{
			LocalizedException ex = null;
			ADUser dataObject = this.DataObject;
			if (!UMSubscriber.IsValidSubscriber(dataObject))
			{
				ex = new UserNotUmEnabledException(this.DataObject.PrimarySmtpAddress.ToString());
			}
			else
			{
				base.PinInfo = base.ValidateOrGeneratePIN(this.Pin);
				base.PinInfo.PinExpired = this.PinExpired;
				base.PinInfo.LockedOut = this.LockedOut;
			}
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x060081E2 RID: 33250 RVA: 0x00212FC8 File Offset: 0x002111C8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.SavePIN();
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				if (this.SendEmail)
				{
					base.SubmitResetPINMessage(this.NotifyEmail);
				}
				if (!this.LockedOut)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMUserUnlocked, null, new object[]
					{
						this.DataObject.PrimarySmtpAddress
					});
				}
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMUserPasswordChanged, null, new object[]
				{
					this.DataObject.PrimarySmtpAddress
				});
				this.WriteResult();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060081E3 RID: 33251 RVA: 0x00213070 File Offset: 0x00211270
		private void WriteResult()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Id
			});
			UMMailboxPin sendToPipeline = new UMMailboxPin(this.DataObject, base.PinInfo.PinExpired, base.PinInfo.LockedOut, base.PinInfo.FirstTimeUser, base.NeedSuppressingPiiData);
			base.WriteObject(sendToPipeline);
			TaskLogger.LogExit();
		}
	}
}
