using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000023 RID: 35
	internal class ReconciliationAccountConfig
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x0000BA0C File Offset: 0x00009C0C
		public static ReconciliationAccountConfig Create(IRecipientSession recipientSession, JournalingReconciliationAccount journalingReconciliationAccount)
		{
			journalingReconciliationAccount.Guid.ToString();
			MultiValuedProperty<ADObjectId> multiValuedProperty = journalingReconciliationAccount.Mailboxes;
			if (multiValuedProperty == null || multiValuedProperty.Count == 0)
			{
				string text = string.Format("No reconcile mailboxes for reconciliation-account: {0}", journalingReconciliationAccount.Id);
				ExTraceGlobals.JournalingTracer.TraceError(0L, text);
				throw new JournalingConfigurationLoadException(text);
			}
			string[] array = new string[multiValuedProperty.Count];
			for (int i = 0; i < multiValuedProperty.Count; i++)
			{
				ADRecipient adrecipient = recipientSession.Read(multiValuedProperty[i]);
				if (adrecipient == null)
				{
					string text2 = string.Format("No recipient with ADObjectId {0} ", multiValuedProperty[i]);
					ExTraceGlobals.JournalingTracer.TraceError(0L, text2);
					throw new JournalingConfigurationLoadException(text2);
				}
				if (adrecipient.RecipientType != RecipientType.UserMailbox)
				{
					string text2 = string.Format("User {0} was not a mailbox user. Actual type: {1}", adrecipient.DistinguishedName, adrecipient.RecipientType);
					ExTraceGlobals.JournalingTracer.TraceError(0L, text2);
					throw new JournalingConfigurationLoadException(text2);
				}
				if (!adrecipient.PrimarySmtpAddress.IsValidAddress)
				{
					string text2 = string.Format("Recipient primary-SMTP address {0} is not a valid SMTP address", adrecipient.PrimarySmtpAddress);
					ExTraceGlobals.JournalingTracer.TraceError(0L, text2);
					throw new JournalingConfigurationLoadException(text2);
				}
				array[i] = adrecipient.PrimarySmtpAddress.ToString();
			}
			return new ReconciliationAccountConfig(array);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000BB64 File Offset: 0x00009D64
		public ReconciliationAccountConfig(string[] mailboxes)
		{
			if (mailboxes == null || mailboxes.Length == 0)
			{
				throw new JournalingConfigurationLoadException("No reconcile mailboxes for reconciliation-account");
			}
			foreach (string text in mailboxes)
			{
				if (string.IsNullOrEmpty(text) || !SmtpAddress.IsValidSmtpAddress(text) || SmtpAddress.NullReversePath.ToString() == text)
				{
					throw new JournalingConfigurationLoadException(string.Format("Invalid reconcile mailbox {0} for reconciliation-account", text));
				}
			}
			this.mailboxes = mailboxes;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000BBE1 File Offset: 0x00009DE1
		public string GetNextMailbox()
		{
			if (this.mailboxes == null || this.mailboxes.Length == 0)
			{
				return null;
			}
			return this.mailboxes[ReconciliationAccountConfig.random.Next(this.mailboxes.Length)];
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000BC10 File Offset: 0x00009E10
		public string[] Mailboxes
		{
			get
			{
				return this.mailboxes;
			}
		}

		// Token: 0x040000C9 RID: 201
		private static readonly Random random = new Random(DateTime.UtcNow.Millisecond);

		// Token: 0x040000CA RID: 202
		private string[] mailboxes;
	}
}
