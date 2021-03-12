﻿using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D08 RID: 3336
	public abstract class UMMailboxTask<TIdentity> : RecipientObjectActionTask<TIdentity, ADUser> where TIdentity : IIdentityParameter, new()
	{
		// Token: 0x170027B7 RID: 10167
		// (get) Token: 0x0600801D RID: 32797 RVA: 0x0020BE2E File Offset: 0x0020A02E
		// (set) Token: 0x0600801E RID: 32798 RVA: 0x0020BE36 File Offset: 0x0020A036
		[Parameter]
		public SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return base.InternalIgnoreDefaultScope;
			}
			set
			{
				base.InternalIgnoreDefaultScope = value;
			}
		}

		// Token: 0x170027B8 RID: 10168
		// (get) Token: 0x0600801F RID: 32799 RVA: 0x0020BE3F File Offset: 0x0020A03F
		// (set) Token: 0x06008020 RID: 32800 RVA: 0x0020BE47 File Offset: 0x0020A047
		protected PINInfo PinInfo
		{
			get
			{
				return this.pinInfo;
			}
			set
			{
				this.pinInfo = value;
			}
		}

		// Token: 0x06008021 RID: 32801 RVA: 0x0020BE50 File Offset: 0x0020A050
		protected override IConfigurable PrepareDataObject()
		{
			ADUser aduser = (ADUser)base.PrepareDataObject();
			IRecipientSession recipientSession = (IRecipientSession)base.DataSession;
			this.DebugTrace("{0}: ADUser.OriginatingServer:{1} - UseGlobalCatalog:{2}", new object[]
			{
				base.GetType().Name,
				aduser.OriginatingServer,
				recipientSession.UseGlobalCatalog
			});
			if (recipientSession.UseGlobalCatalog)
			{
				recipientSession.UseGlobalCatalog = false;
				this.DebugTrace("{0}: Reading {1}", new object[]
				{
					base.GetType().Name,
					aduser.Id
				});
				aduser = (ADUser)recipientSession.Read(aduser.Id);
				if (aduser == null)
				{
					this.WriteObjectNotFoundError();
				}
				recipientSession.UseGlobalCatalog = true;
				this.DebugTrace("{0}: ADUser.OriginatingServer(after read):{1}", new object[]
				{
					base.GetType().Name,
					aduser.OriginatingServer
				});
			}
			return aduser;
		}

		// Token: 0x06008022 RID: 32802 RVA: 0x0020BF3C File Offset: 0x0020A13C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			try
			{
				if (!base.HasErrors)
				{
					if (this.DataObject.RecipientTypeDetails != RecipientTypeDetails.MailboxPlan)
					{
						ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(this.DataObject, null);
						if (exchangePrincipal != null && !CommonUtil.IsServerCompatible(exchangePrincipal.MailboxInfo.Location.ServerVersion))
						{
							base.WriteError(new OperationNotSupportedOnLegacMailboxException(this.DataObject.Id.Name), ErrorCategory.InvalidOperation, this.DataObject);
						}
					}
					this.DoValidate();
				}
			}
			catch (ADTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.NotSpecified, null);
			}
			catch (ADOperationException exception2)
			{
				base.WriteError(exception2, ErrorCategory.NotSpecified, null);
			}
			catch (DataValidationException exception3)
			{
				base.WriteError(exception3, ErrorCategory.NotSpecified, null);
			}
			catch (StoragePermanentException exception4)
			{
				base.WriteError(exception4, ErrorCategory.NotSpecified, null);
			}
			catch (StorageTransientException exception5)
			{
				base.WriteError(exception5, ErrorCategory.NotSpecified, null);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06008023 RID: 32803 RVA: 0x0020C05C File Offset: 0x0020A25C
		protected virtual void DoValidate()
		{
		}

		// Token: 0x06008024 RID: 32804 RVA: 0x0020C060 File Offset: 0x0020A260
		protected PINInfo ValidateOrGeneratePIN(string pin, Guid umUserMailboxPolicyGuid)
		{
			ADUser dataObject = this.DataObject;
			PINInfo pininfo = null;
			try
			{
				using (IUMUserMailboxStorage umuserMailboxAccessor = InterServerMailboxAccessor.GetUMUserMailboxAccessor(dataObject, false))
				{
					pininfo = umuserMailboxAccessor.ValidateUMPin(pin, umUserMailboxPolicyGuid);
				}
			}
			catch (LocalizedException ex)
			{
				base.WriteError(new RecipientTaskException(Strings.ValidateGeneratePINError(dataObject.PrimarySmtpAddress.ToString(), ex.LocalizedString), ex), ErrorCategory.NotSpecified, null);
			}
			if (!pininfo.IsValid)
			{
				if (string.IsNullOrEmpty(pin))
				{
					base.WriteError(new DefaultPinGenerationException(), ErrorCategory.NotSpecified, null);
				}
				else
				{
					base.WriteError(this.CreateWeakPinException(dataObject), ErrorCategory.InvalidArgument, null);
				}
			}
			return pininfo;
		}

		// Token: 0x06008025 RID: 32805 RVA: 0x0020C114 File Offset: 0x0020A314
		protected PINInfo ValidateOrGeneratePIN(string pin)
		{
			return this.ValidateOrGeneratePIN(pin, Guid.Empty);
		}

		// Token: 0x06008026 RID: 32806 RVA: 0x0020C122 File Offset: 0x0020A322
		protected void SavePIN()
		{
			this.SavePIN(Guid.Empty);
		}

		// Token: 0x06008027 RID: 32807 RVA: 0x0020C130 File Offset: 0x0020A330
		protected void SavePIN(Guid umUserMailboxPolicyGuid)
		{
			ADUser dataObject = this.DataObject;
			try
			{
				using (IUMUserMailboxStorage umuserMailboxAccessor = InterServerMailboxAccessor.GetUMUserMailboxAccessor(dataObject, false))
				{
					umuserMailboxAccessor.SaveUMPin(this.PinInfo, umUserMailboxPolicyGuid);
				}
			}
			catch (LocalizedException ex)
			{
				base.WriteError(new RecipientTaskException(Strings.SavePINError(dataObject.PrimarySmtpAddress.ToString(), ex.LocalizedString), ex), ErrorCategory.NotSpecified, null);
			}
		}

		// Token: 0x06008028 RID: 32808 RVA: 0x0020C1B4 File Offset: 0x0020A3B4
		protected void SubmitWelcomeMessage(string notifyEmail, string pilotNumber, MultiValuedProperty<string> extensions, UMDialPlan dialPlan)
		{
			ADUser dataObject = this.DataObject;
			if (string.IsNullOrEmpty(notifyEmail))
			{
				notifyEmail = dataObject.PrimarySmtpAddress.ToString();
			}
			string[] accessNumbers = null;
			if (!string.IsNullOrEmpty(pilotNumber))
			{
				accessNumbers = new string[]
				{
					pilotNumber
				};
			}
			else if (dialPlan.AccessTelephoneNumbers != null && dialPlan.AccessTelephoneNumbers.Count > 0)
			{
				accessNumbers = dialPlan.AccessTelephoneNumbers.ToArray();
			}
			string extension = null;
			if (extensions != null && extensions.Count > 0)
			{
				extension = extensions[0];
			}
			try
			{
				Utils.SendWelcomeMail(dataObject, accessNumbers, extension, this.PinInfo.PIN, notifyEmail, null);
			}
			catch (LocalizedException ex)
			{
				base.WriteError(new RecipientTaskException(Strings.SubmitWelcomeMailError(dataObject.PrimarySmtpAddress.ToString(), ex.LocalizedString), ex), ErrorCategory.NotSpecified, null);
			}
		}

		// Token: 0x06008029 RID: 32809 RVA: 0x0020C298 File Offset: 0x0020A498
		protected void SubmitResetPINMessage(string notifyEmail)
		{
			ADUser dataObject = this.DataObject;
			if (string.IsNullOrEmpty(notifyEmail))
			{
				notifyEmail = dataObject.PrimarySmtpAddress.ToString();
			}
			UMMailbox ummailbox = new UMMailbox(dataObject);
			UMDialPlan dialPlan = ummailbox.GetDialPlan();
			string[] accessNumbers = null;
			if (dialPlan.AccessTelephoneNumbers != null && dialPlan.AccessTelephoneNumbers.Count > 0)
			{
				accessNumbers = dialPlan.AccessTelephoneNumbers.ToArray();
			}
			string extension = null;
			if (!Utils.TryGetNumericExtension(dialPlan, dataObject, out extension))
			{
				extension = null;
			}
			try
			{
				Utils.SendPasswordResetMail(dataObject, accessNumbers, extension, this.PinInfo.PIN, notifyEmail);
			}
			catch (LocalizedException ex)
			{
				base.WriteError(new RecipientTaskException(Strings.SendPINResetMailError(dataObject.PrimarySmtpAddress.ToString(), ex.LocalizedString), ex), ErrorCategory.NotSpecified, null);
			}
		}

		// Token: 0x0600802A RID: 32810 RVA: 0x0020C36C File Offset: 0x0020A56C
		protected void InitUMMailbox()
		{
			ADUser dataObject = this.DataObject;
			try
			{
				using (IUMUserMailboxStorage umuserMailboxAccessor = InterServerMailboxAccessor.GetUMUserMailboxAccessor(dataObject, false))
				{
					umuserMailboxAccessor.InitUMMailbox();
				}
			}
			catch (LocalizedException ex)
			{
				base.WriteError(new RecipientTaskException(Strings.InitUMMailboxError(dataObject.PrimarySmtpAddress.ToString(), ex.LocalizedString), ex), ErrorCategory.NotSpecified, null);
			}
		}

		// Token: 0x0600802B RID: 32811 RVA: 0x0020C3E8 File Offset: 0x0020A5E8
		protected void ResetUMMailbox(bool keepProperties)
		{
			ADUser dataObject = this.DataObject;
			try
			{
				using (IUMUserMailboxStorage umuserMailboxAccessor = InterServerMailboxAccessor.GetUMUserMailboxAccessor(dataObject, false))
				{
					umuserMailboxAccessor.ResetUMMailbox(keepProperties);
				}
			}
			catch (LocalizedException ex)
			{
				base.WriteError(new RecipientTaskException(Strings.ResetUMMailboxError(dataObject.PrimarySmtpAddress.ToString(), ex.LocalizedString), ex), ErrorCategory.NotSpecified, null);
			}
		}

		// Token: 0x0600802C RID: 32812 RVA: 0x0020C468 File Offset: 0x0020A668
		protected void DebugTrace(string formatString, params object[] formatObjects)
		{
			ExTraceGlobals.UtilTracer.TraceDebug((long)this.GetHashCode(), formatString, formatObjects);
		}

		// Token: 0x0600802D RID: 32813 RVA: 0x0020C480 File Offset: 0x0020A680
		private void WriteObjectNotFoundError()
		{
			TIdentity identity = this.Identity;
			LocalizedString errorMessageObjectNotFound = base.GetErrorMessageObjectNotFound(identity.ToString(), typeof(ADUser).ToString(), null);
			base.WriteError(new ManagementObjectNotFoundException(errorMessageObjectNotFound), ErrorCategory.InvalidData, this.Identity);
		}

		// Token: 0x0600802E RID: 32814 RVA: 0x0020C4D0 File Offset: 0x0020A6D0
		private WeakPinException CreateWeakPinException(ADUser user)
		{
			UMMailboxPolicy policy = new UMMailbox(user).GetPolicy();
			LocalizedString localizedString = LocalizedString.Empty;
			if (policy.PINHistoryCount == 0)
			{
				localizedString = Strings.ErrorWeakPasswordNoHistory(policy.MinPINLength);
			}
			else if (policy.PINHistoryCount == 1)
			{
				localizedString = Strings.ErrorWeakPasswordHistorySingular(policy.MinPINLength);
			}
			else
			{
				localizedString = Strings.ErrorWeakPasswordHistoryPlural(policy.MinPINLength, policy.PINHistoryCount);
			}
			if (!policy.AllowCommonPatterns)
			{
				localizedString = Strings.ErrorWeakPasswordWithNoCommonPatterns(localizedString);
			}
			return new WeakPinException(localizedString);
		}

		// Token: 0x04003ED5 RID: 16085
		private PINInfo pinInfo;
	}
}
