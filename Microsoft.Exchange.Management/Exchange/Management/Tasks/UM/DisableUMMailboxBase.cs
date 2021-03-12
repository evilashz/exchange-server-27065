using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D09 RID: 3337
	public class DisableUMMailboxBase<TIdentity> : UMMailboxTask<TIdentity> where TIdentity : RecipientIdParameter, new()
	{
		// Token: 0x06008030 RID: 32816 RVA: 0x0020C550 File Offset: 0x0020A750
		public DisableUMMailboxBase()
		{
			this.KeepProperties = true;
		}

		// Token: 0x170027B9 RID: 10169
		// (get) Token: 0x06008031 RID: 32817 RVA: 0x0020C55F File Offset: 0x0020A75F
		// (set) Token: 0x06008032 RID: 32818 RVA: 0x0020C576 File Offset: 0x0020A776
		[LocDescription(Strings.IDs.KeepProperties)]
		[Parameter(Mandatory = false)]
		public bool KeepProperties
		{
			get
			{
				return (bool)base.Fields["KeepProperties"];
			}
			set
			{
				base.Fields["KeepProperties"] = value;
			}
		}

		// Token: 0x170027BA RID: 10170
		// (get) Token: 0x06008033 RID: 32819 RVA: 0x0020C590 File Offset: 0x0020A790
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				TIdentity identity = this.Identity;
				return Strings.ConfirmationMessageDisableUMMailbox(identity.ToString());
			}
		}

		// Token: 0x06008034 RID: 32820 RVA: 0x0020C5B8 File Offset: 0x0020A7B8
		protected override void DoValidate()
		{
			LocalizedException ex = null;
			if (!UMSubscriber.IsValidSubscriber(this.DataObject))
			{
				ex = new UserAlreadyUmDisabledException(this.DataObject.PrimarySmtpAddress.ToString());
			}
			else
			{
				Utility.ResetUMADProperties(this.DataObject, this.KeepProperties);
			}
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06008035 RID: 32821 RVA: 0x0020C614 File Offset: 0x0020A814
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.ResetUMMailbox(this.KeepProperties);
			base.InternalProcessRecord();
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMUserDisabled, null, new object[]
			{
				this.DataObject.PrimarySmtpAddress
			});
			this.WriteResult();
			TaskLogger.LogExit();
		}

		// Token: 0x06008036 RID: 32822 RVA: 0x0020C670 File Offset: 0x0020A870
		private void WriteResult()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Id
			});
			UMMailbox sendToPipeline = new UMMailbox(this.DataObject);
			base.WriteObject(sendToPipeline);
			TaskLogger.LogExit();
		}

		// Token: 0x04003ED6 RID: 16086
		internal static readonly PropertyDefinition[] PropertiesToReset = new PropertyDefinition[]
		{
			ADUserSchema.UMEnabledFlags,
			ADUserSchema.UMEnabledFlags2,
			ADUserSchema.UMMailboxPolicy,
			ADUserSchema.OperatorNumber,
			ADUserSchema.PhoneProviderId,
			ADUserSchema.UMPinChecksum,
			ADUserSchema.UMServerWritableFlags,
			ADUserSchema.CallAnsweringAudioCodecLegacy,
			ADUserSchema.CallAnsweringAudioCodec2
		};
	}
}
