using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000125 RID: 293
	public abstract class NewMailEnabledRecipientObjectTask<TDataObject> : NewRecipientObjectTask<TDataObject> where TDataObject : ADRecipient, new()
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0002FB40 File Offset: 0x0002DD40
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x0002FB57 File Offset: 0x0002DD57
		[Parameter(Mandatory = false)]
		public virtual MailboxIdParameter ArbitrationMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields[ADRecipientSchema.ArbitrationMailbox];
			}
			set
			{
				base.Fields[ADRecipientSchema.ArbitrationMailbox] = value;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0002FB6A File Offset: 0x0002DD6A
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x0002FB81 File Offset: 0x0002DD81
		[Parameter(Mandatory = false)]
		public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
		{
			get
			{
				return (MultiValuedProperty<ModeratorIDParameter>)base.Fields[ADRecipientSchema.ModeratedBy];
			}
			set
			{
				base.Fields[ADRecipientSchema.ModeratedBy] = value;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0002FB94 File Offset: 0x0002DD94
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x0002FBB8 File Offset: 0x0002DDB8
		[Parameter]
		public virtual bool ModerationEnabled
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.ModerationEnabled;
			}
			set
			{
				TDataObject dataObject = this.DataObject;
				dataObject.ModerationEnabled = value;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0002FBDC File Offset: 0x0002DDDC
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x0002FC00 File Offset: 0x0002DE00
		[Parameter]
		public virtual TransportModerationNotificationFlags SendModerationNotifications
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.SendModerationNotifications;
			}
			set
			{
				TDataObject dataObject = this.DataObject;
				dataObject.SendModerationNotifications = value;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0002FC22 File Offset: 0x0002DE22
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x0002FC48 File Offset: 0x0002DE48
		[Parameter]
		public SwitchParameter OverrideRecipientQuotas
		{
			get
			{
				return (SwitchParameter)(base.Fields["OverrideRecipientQuotas"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["OverrideRecipientQuotas"] = value;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0002FC60 File Offset: 0x0002DE60
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x0002FC77 File Offset: 0x0002DE77
		public virtual MultiValuedProperty<string> MailTipTranslations
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields[ADRecipientSchema.MailTipTranslations];
			}
			set
			{
				base.Fields[ADRecipientSchema.MailTipTranslations] = value;
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002FC8C File Offset: 0x0002DE8C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (base.Fields.IsModified(ADRecipientSchema.MailTipTranslations) && this.MailTipTranslations != null && this.MailTipTranslations.Count > 0)
			{
				TDataObject dataObject = this.DataObject;
				dataObject.MailTipTranslations = MailboxTaskHelper.ValidateAndSanitizeTranslations(this.MailTipTranslations, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002FCFC File Offset: 0x0002DEFC
		protected override void PrepareRecipientObject(TDataObject recipient)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(recipient);
			if (base.Fields.Contains(ADRecipientSchema.ModeratedBy))
			{
				MultiValuedProperty<ModeratorIDParameter> multiValuedProperty = (MultiValuedProperty<ModeratorIDParameter>)base.Fields[ADRecipientSchema.ModeratedBy];
				int num = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled ? 10 : 25;
				if (multiValuedProperty != null && multiValuedProperty.Count > num)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorTooManyModerators(num)), ExchangeErrorCategory.Client, null);
				}
				recipient.ModeratedBy = RecipientTaskHelper.GetModeratedByAdObjectIdFromParameterID(base.TenantGlobalCatalogSession, this.ModeratedBy, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), recipient, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(ADRecipientSchema.ArbitrationMailbox))
			{
				if (this.ArbitrationMailbox != null)
				{
					ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(this.ArbitrationMailbox, (IRecipientSession)base.DataSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(this.ArbitrationMailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(this.ArbitrationMailbox.ToString())), ExchangeErrorCategory.Client);
					if (adrecipient.RecipientTypeDetails != RecipientTypeDetails.ArbitrationMailbox)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorInvalidArbitrationMbxType(adrecipient.Identity.ToString())), ExchangeErrorCategory.Client, recipient.Identity);
					}
					if (MultiValuedPropertyBase.IsNullOrEmpty((ADMultiValuedProperty<ADObjectId>)adrecipient[ADUserSchema.ApprovalApplications]))
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorInvalidArbitrationMbxTypeForModerationAndAutogroup(adrecipient.Identity.ToString())), ExchangeErrorCategory.Client, recipient.Identity);
					}
					if (!recipient.OrganizationId.Equals(adrecipient.OrganizationId))
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorArbitrationMbxCrossOrg(adrecipient.Identity.ToString())), ExchangeErrorCategory.Client, recipient.Identity);
					}
					recipient.ArbitrationMailbox = adrecipient.Id;
				}
				else
				{
					recipient.ArbitrationMailbox = null;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002FF21 File Offset: 0x0002E121
		[Obsolete("Use GetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, OptionalIdentityData optionalData, LocalizedString? notFoundError, LocalizedString? multipleFoundError, ExchangeErrorCategory errorCategory) instead")]
		protected new IConfigurable GetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, OptionalIdentityData optionalData, LocalizedString? notFoundError, LocalizedString? multipleFoundError) where TObject : IConfigurable, new()
		{
			return base.GetDataObject<TObject>(id, session, rootID, optionalData, notFoundError, multipleFoundError);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002FF32 File Offset: 0x0002E132
		[Obsolete("Use GetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, LocalizedString? notFoundError, LocalizedString? multipleFoundError, ExchangeErrorCategory errorCategory) instead")]
		protected new IConfigurable GetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, LocalizedString? notFoundError, LocalizedString? multipleFoundError) where TObject : IConfigurable, new()
		{
			return base.GetDataObject<TObject>(id, session, rootID, null, notFoundError, multipleFoundError);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002FF42 File Offset: 0x0002E142
		[Obsolete("Use ThrowTerminatingError(Exception exception, ExchangeErrorCategory category, object target) instead.")]
		protected new void ThrowTerminatingError(Exception exception, ErrorCategory category, object target)
		{
			base.ThrowTerminatingError(exception, category, target);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002FF4D File Offset: 0x0002E14D
		[Obsolete("Use WriteError(Exception exception, ExchangeErrorCategory category, object target, bool reThrow) instead.")]
		protected new void WriteError(Exception exception, ErrorCategory category, object target, bool reThrow)
		{
			base.WriteError(exception, category, target, reThrow);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002FF5A File Offset: 0x0002E15A
		[Obsolete("Use WriteError(Exception exception, ExchangeErrorCategory category, object target) instead.")]
		internal new void WriteError(Exception exception, ErrorCategory category, object target)
		{
			base.WriteError(exception, category, target, true);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002FF66 File Offset: 0x0002E166
		[Obsolete("Use WriteError(Exception exception, ExchangeErrorCategory category, object target, bool reThrow, string helpUrl) instead.")]
		protected new void WriteError(Exception exception, ErrorCategory category, object target, bool reThrow, string helpUrl)
		{
			base.WriteError(exception, category, target, reThrow, helpUrl);
		}
	}
}
