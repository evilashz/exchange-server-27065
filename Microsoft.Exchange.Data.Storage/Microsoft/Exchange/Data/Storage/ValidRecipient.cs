using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DE7 RID: 3559
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ValidRecipient
	{
		// Token: 0x06007A64 RID: 31332 RVA: 0x0021D348 File Offset: 0x0021B548
		internal ValidRecipient(string smtpAddress, ADRecipient adRecipient)
		{
			Util.ThrowOnNullOrEmptyArgument(smtpAddress, "smtpAddress");
			if (!Microsoft.Exchange.Data.SmtpAddress.IsValidSmtpAddress(smtpAddress))
			{
				throw new ArgumentOutOfRangeException(ServerStrings.InvalidSmtpAddress(smtpAddress));
			}
			this.SmtpAddress = smtpAddress;
			this.ADRecipient = adRecipient;
			SmtpProxyAddress smtpProxyAddress = (adRecipient != null) ? (adRecipient.ExternalEmailAddress as SmtpProxyAddress) : null;
			if (smtpProxyAddress != null)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<ADObjectId, SmtpProxyAddress>((long)this.GetHashCode(), "{0}: using ExternalEmailAddress as SmtpAddress: {1}", adRecipient.Id, smtpProxyAddress);
				this.SmtpAddressForEncryption = smtpProxyAddress.SmtpAddress;
				return;
			}
			this.SmtpAddressForEncryption = smtpAddress;
		}

		// Token: 0x170020BD RID: 8381
		// (get) Token: 0x06007A65 RID: 31333 RVA: 0x0021D3D9 File Offset: 0x0021B5D9
		// (set) Token: 0x06007A66 RID: 31334 RVA: 0x0021D3E1 File Offset: 0x0021B5E1
		internal string SmtpAddress { get; private set; }

		// Token: 0x170020BE RID: 8382
		// (get) Token: 0x06007A67 RID: 31335 RVA: 0x0021D3EA File Offset: 0x0021B5EA
		// (set) Token: 0x06007A68 RID: 31336 RVA: 0x0021D3F2 File Offset: 0x0021B5F2
		internal string SmtpAddressForEncryption { get; private set; }

		// Token: 0x170020BF RID: 8383
		// (get) Token: 0x06007A69 RID: 31337 RVA: 0x0021D3FB File Offset: 0x0021B5FB
		// (set) Token: 0x06007A6A RID: 31338 RVA: 0x0021D403 File Offset: 0x0021B603
		internal ADRecipient ADRecipient { get; private set; }

		// Token: 0x06007A6B RID: 31339 RVA: 0x0021D40C File Offset: 0x0021B60C
		public override string ToString()
		{
			return this.SmtpAddress;
		}

		// Token: 0x06007A6C RID: 31340 RVA: 0x0021D41C File Offset: 0x0021B61C
		internal static string[] ConvertToStringArray(ValidRecipient[] recipients)
		{
			return Array.ConvertAll<ValidRecipient, string>(recipients, (ValidRecipient recipient) => recipient.ToString());
		}

		// Token: 0x06007A6D RID: 31341 RVA: 0x0021D44A File Offset: 0x0021B64A
		internal static ValidRecipient[] ConvertFromStringArray(string[] recipients)
		{
			return Array.ConvertAll<string, ValidRecipient>(recipients, (string recipient) => new ValidRecipient(recipient, null));
		}

		// Token: 0x04005449 RID: 21577
		internal static readonly ValidRecipient[] EmptyRecipients = Array<ValidRecipient>.Empty;
	}
}
