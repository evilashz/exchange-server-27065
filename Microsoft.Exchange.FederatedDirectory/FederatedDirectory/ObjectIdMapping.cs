using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x0200000D RID: 13
	internal sealed class ObjectIdMapping
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003CFC File Offset: 0x00001EFC
		public ObjectIdMapping(IRecipientSession recipientSession)
		{
			this.recipientSession = recipientSession;
			this.smtpAddresses = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this.InvalidSmtpAddresses = new List<string>(2);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003D27 File Offset: 0x00001F27
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003D2F File Offset: 0x00001F2F
		public List<string> InvalidSmtpAddresses { get; private set; }

		// Token: 0x06000087 RID: 135 RVA: 0x00003D38 File Offset: 0x00001F38
		public void Prefetch(params string[] smtpAddresses)
		{
			if (smtpAddresses != null)
			{
				this.smtpAddresses.UnionWith(smtpAddresses);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003D4C File Offset: 0x00001F4C
		public string GetIdentityFromSmtpAddress(string smtpAddress)
		{
			this.InitializeIfNeeded();
			Guid guid;
			if (this.smtpAddressToIdentity.TryGetValue(smtpAddress, out guid))
			{
				return guid.ToString();
			}
			ObjectIdMapping.Tracer.TraceWarning<string>((long)this.GetHashCode(), "IdentityMapping.GetIdentityFromSmtpAddress: unable to retrieve identity for object with SMTP address {0}", smtpAddress);
			return Guid.Empty.ToString();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003DA8 File Offset: 0x00001FA8
		public string[] GetIdentitiesFromSmtpAddresses(string[] smtpAddresses)
		{
			if (smtpAddresses == null)
			{
				return null;
			}
			this.InitializeIfNeeded();
			List<string> list = new List<string>(smtpAddresses.Length);
			foreach (string text in smtpAddresses)
			{
				Guid guid;
				if (this.smtpAddressToIdentity.TryGetValue(text, out guid))
				{
					list.Add(guid.ToString());
				}
				else
				{
					ObjectIdMapping.Tracer.TraceWarning<string>((long)this.GetHashCode(), "IdentityMapping.GetIdentityFromSmtpAddress: unable to retrieve identity for object with SMTP address {0}", text);
					list.Add(Guid.Empty.ToString());
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003E40 File Offset: 0x00002040
		public string GetSmtpAddressFromIdentity(string identity)
		{
			this.InitializeIfNeeded();
			string result;
			if (this.identityToSmtpAddress.TryGetValue(identity, out result))
			{
				return result;
			}
			ObjectIdMapping.Tracer.TraceWarning<string>((long)this.GetHashCode(), "IdentityMapping.GetSmtpAddressFromIdentity: unable to retrieve SMTP address for object with identity {0}", identity);
			return string.Empty;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003E8C File Offset: 0x0000208C
		private void InitializeIfNeeded()
		{
			if (this.smtpAddressToIdentity != null)
			{
				return;
			}
			this.smtpAddresses.Remove(null);
			List<ProxyAddress> list = new List<ProxyAddress>(this.smtpAddresses.Count);
			foreach (string text in this.smtpAddresses)
			{
				ProxyAddress item;
				if (ProxyAddress.TryParse(text, out item))
				{
					list.Add(item);
				}
				else
				{
					this.InvalidSmtpAddresses.Add(text);
				}
			}
			if (ObjectIdMapping.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ObjectIdMapping.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "IdentityMapping.InitializeIfNeeded: performing lookup in AD. SMTP addresses={0}, LegacyDNs={1}", string.Join<ProxyAddress>(",", list), string.Join(",", this.InvalidSmtpAddresses));
			}
			this.smtpAddressToIdentity = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase);
			this.identityToSmtpAddress = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			if (list.Count > 0)
			{
				this.ResolveExternalIdForExchangeIdentity<ProxyAddress>(list.ToArray(), (ProxyAddress proxyAddress) => proxyAddress.AddressString, new Func<ProxyAddress[], PropertyDefinition[], Result<ADRawEntry>[]>(this.recipientSession.FindByProxyAddresses));
			}
			if (this.InvalidSmtpAddresses.Count > 0)
			{
				this.ResolveExternalIdForExchangeIdentity<string>(this.InvalidSmtpAddresses.ToArray(), (string legacyDn) => legacyDn, new Func<string[], PropertyDefinition[], Result<ADRawEntry>[]>(this.recipientSession.FindByLegacyExchangeDNs));
			}
			this.smtpAddresses.Clear();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000401C File Offset: 0x0000221C
		private void ResolveExternalIdForExchangeIdentity<T>(T[] exchangeIdentities, Func<T, string> keySelector, Func<T[], PropertyDefinition[], Result<ADRawEntry>[]> directoryLookup)
		{
			Result<ADRawEntry>[] array = directoryLookup(exchangeIdentities, new ADPropertyDefinition[]
			{
				ADRecipientSchema.ExternalDirectoryObjectId
			});
			for (int i = 0; i < exchangeIdentities.Length; i++)
			{
				string text = keySelector(exchangeIdentities[i]);
				Result<ADRawEntry> result = array[i];
				if (result.Data == null)
				{
					ObjectIdMapping.Tracer.TraceError<string, ProviderError>((long)this.GetHashCode(), "IdentityMapping.ResolveExternalIdForExchangeIdentity: Lookup AD object for '{0}' failed due to error: {1}", text, result.Error);
				}
				else
				{
					string text2 = result.Data[ADRecipientSchema.ExternalDirectoryObjectId] as string;
					Guid guid;
					if (text2 != null && Guid.TryParse(text2, out guid))
					{
						this.smtpAddressToIdentity.Add(text, guid);
						this.identityToSmtpAddress.Add(guid.ToString(), text);
						ObjectIdMapping.Tracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "IdentityMapping.ResolveExternalIdForExchangeIdentity: AD object for '{0}' maps to ExternalDirectoryObjectId '{1}'", text, guid);
					}
					else
					{
						ObjectIdMapping.Tracer.TraceError<string>((long)this.GetHashCode(), "IdentityMapping.ResolveExternalIdForExchangeIdentity: ExternalDirectoryObjectId is either empty or not valid guid. Key='{0}'", text);
					}
				}
			}
		}

		// Token: 0x0400004D RID: 77
		private static readonly Trace Tracer = ExTraceGlobals.ModernGroupsTracer;

		// Token: 0x0400004E RID: 78
		private readonly IRecipientSession recipientSession;

		// Token: 0x0400004F RID: 79
		private readonly HashSet<string> smtpAddresses;

		// Token: 0x04000050 RID: 80
		private Dictionary<string, Guid> smtpAddressToIdentity;

		// Token: 0x04000051 RID: 81
		private Dictionary<string, string> identityToSmtpAddress;
	}
}
