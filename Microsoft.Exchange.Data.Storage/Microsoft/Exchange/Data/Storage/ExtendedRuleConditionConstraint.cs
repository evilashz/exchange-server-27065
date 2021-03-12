using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BC0 RID: 3008
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class ExtendedRuleConditionConstraint : StoreObjectConstraint
	{
		// Token: 0x06006B6D RID: 27501 RVA: 0x001CBD08 File Offset: 0x001C9F08
		private static int GetExtendedRuleSizeLimit(StoreSession storeSession)
		{
			storeSession.Mailbox.Load(new PropertyDefinition[]
			{
				MailboxSchema.ExtendedRuleSizeLimit
			});
			return storeSession.Mailbox.GetValueOrDefault<int>(MailboxSchema.ExtendedRuleSizeLimit, 522240);
		}

		// Token: 0x06006B6E RID: 27502 RVA: 0x001CBD50 File Offset: 0x001C9F50
		public static void InitExtendedRuleSizeLimitIfNeeded(MailboxSession originalSession)
		{
			if (originalSession.LogonType != LogonType.BestAccess && originalSession.LogonType != LogonType.Delegated && originalSession.LogonType != LogonType.Owner)
			{
				return;
			}
			int? num = originalSession.Mailbox.TryGetProperty(MailboxSchema.ExtendedRuleSizeLimit) as int?;
			if (num == null || num > 522240)
			{
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(originalSession.MailboxOwner, originalSession.InternalCulture, "Client=Management;Action=InitExtendedRuleSizeLimit"))
				{
					mailboxSession.Mailbox.SetProperties(new PropertyDefinition[]
					{
						MailboxSchema.ExtendedRuleSizeLimit
					}, new object[]
					{
						522240
					});
					CoreObject.GetPersistablePropertyBag((CoreMailboxObject)mailboxSession.Mailbox.CoreObject).FlushChanges();
				}
				originalSession.Mailbox.ForceReload(new PropertyDefinition[]
				{
					MailboxSchema.ExtendedRuleSizeLimit
				});
			}
		}

		// Token: 0x06006B6F RID: 27503 RVA: 0x001CBE5C File Offset: 0x001CA05C
		public static void ValidateStreamIfApplicable(long streamLength, PropertyDefinition propertyDefinition, StoreObjectPropertyBag propertyBag)
		{
			if (ExtendedRuleConditionConstraint.propertyDefinition.Equals(propertyDefinition) && propertyBag != null && propertyBag.MapiPropertyBag != null && propertyBag.MapiPropertyBag.StoreSession != null)
			{
				int extendedRuleSizeLimit = ExtendedRuleConditionConstraint.GetExtendedRuleSizeLimit(propertyBag.MapiPropertyBag.StoreSession);
				if (streamLength > (long)extendedRuleSizeLimit)
				{
					throw new StoragePermanentException(ServerStrings.ExConstraintViolationByteArrayLengthTooLong(propertyDefinition.Name, (long)extendedRuleSizeLimit, streamLength));
				}
			}
		}

		// Token: 0x06006B70 RID: 27504 RVA: 0x001CBEB8 File Offset: 0x001CA0B8
		public ExtendedRuleConditionConstraint() : base(new PropertyDefinition[]
		{
			ExtendedRuleConditionConstraint.propertyDefinition
		})
		{
		}

		// Token: 0x06006B71 RID: 27505 RVA: 0x001CBEDC File Offset: 0x001CA0DC
		internal override StoreObjectValidationError Validate(ValidationContext context, IValidatablePropertyBag validatablePropertyBag)
		{
			if (validatablePropertyBag is PropertyBag && validatablePropertyBag.IsPropertyDirty(ExtendedRuleConditionConstraint.propertyDefinition))
			{
				byte[] array = validatablePropertyBag.TryGetProperty(ExtendedRuleConditionConstraint.propertyDefinition) as byte[];
				StoreSession session = ((PropertyBag)validatablePropertyBag).Context.Session;
				if (array != null && session != null && array.Length > ExtendedRuleConditionConstraint.GetExtendedRuleSizeLimit(session))
				{
					return new StoreObjectValidationError(context, ExtendedRuleConditionConstraint.propertyDefinition, array, this);
				}
			}
			return null;
		}

		// Token: 0x06006B72 RID: 27506 RVA: 0x001CBF40 File Offset: 0x001CA140
		public override string ToString()
		{
			return string.Format("Property {0} has length constraint.", ExtendedRuleConditionConstraint.propertyDefinition.Name);
		}

		// Token: 0x04003D71 RID: 15729
		private const string InitExtendedRuleSizeLimitSessionClientString = "Client=Management;Action=InitExtendedRuleSizeLimit";

		// Token: 0x04003D72 RID: 15730
		private const int DefaultExtendedRuleSizeLimit = 522240;

		// Token: 0x04003D73 RID: 15731
		private static readonly StorePropertyDefinition propertyDefinition = InternalSchema.ExtendedRuleCondition;
	}
}
