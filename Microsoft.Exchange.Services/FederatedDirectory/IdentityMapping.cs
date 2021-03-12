using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Office.Server.Directory;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x0200007A RID: 122
	internal sealed class IdentityMapping
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0000F28E File Offset: 0x0000D48E
		public IdentityMapping(IRecipientSession recipientSession)
		{
			this.recipientSession = recipientSession;
			this.smtpAddresses = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000F2AD File Offset: 0x0000D4AD
		public void Prefetch(params string[] smtpAddresses)
		{
			this.smtpAddresses.UnionWith(smtpAddresses);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000F2BC File Offset: 0x0000D4BC
		public Guid GetIdentityFromSmtpAddress(string smtpAddress)
		{
			this.InitializeIfNeeded();
			Guid result;
			if (this.smtpAddressToIdentity.TryGetValue(smtpAddress, out result))
			{
				return result;
			}
			LogWriter.TraceAndLog(new LogWriter.TraceMethod(IdentityMapping.Tracer.TraceWarning), 1, this.GetHashCode(), "IdentityMapping.GetIdentityFromSmtpAddress: unable to retrieve identity for object with SMTP address {0}", new object[]
			{
				smtpAddress
			});
			return Guid.Empty;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000F314 File Offset: 0x0000D514
		public void AddToRelation(string[] smtpAddresses, RelationSet relationSet)
		{
			this.InitializeIfNeeded();
			if (smtpAddresses != null && smtpAddresses.Length > 0)
			{
				foreach (string text in smtpAddresses)
				{
					Guid guid;
					if (this.smtpAddressToIdentity.TryGetValue(text, out guid))
					{
						if (!IdentityMapping.RelationSetContains(relationSet, guid))
						{
							relationSet.Add(guid, 1);
						}
					}
					else
					{
						LogWriter.TraceAndLog(new LogWriter.TraceMethod(IdentityMapping.Tracer.TraceWarning), 1, this.GetHashCode(), "IdentityMapping.AddToRelation: unable to retrieve identity for object with SMTP address {0}", new object[]
						{
							text
						});
					}
				}
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000F398 File Offset: 0x0000D598
		public static bool RelationSetContains(RelationSet relationSet, Guid identity)
		{
			foreach (Relation relation in relationSet)
			{
				if (relation.TargetObjectId.Equals(identity))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000F3F4 File Offset: 0x0000D5F4
		public void RemoveFromRelation(string[] smtpAddresses, RelationSet relationSet)
		{
			this.InitializeIfNeeded();
			if (smtpAddresses != null && smtpAddresses.Length > 0)
			{
				foreach (string text in smtpAddresses)
				{
					Guid guid;
					if (this.smtpAddressToIdentity.TryGetValue(text, out guid))
					{
						relationSet.Remove(guid);
					}
					else
					{
						LogWriter.TraceAndLog(new LogWriter.TraceMethod(IdentityMapping.Tracer.TraceWarning), 1, this.GetHashCode(), "IdentityMapping.RemoveFromRelation: unable to retrieve identity for object with SMTP address {0}", new object[]
						{
							text
						});
					}
				}
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000F470 File Offset: 0x0000D670
		private void InitializeIfNeeded()
		{
			if (this.smtpAddressToIdentity != null)
			{
				return;
			}
			SmtpProxyAddress[] array = new SmtpProxyAddress[this.smtpAddresses.Count];
			int num = 0;
			foreach (string address in this.smtpAddresses)
			{
				array[num] = new SmtpProxyAddress(address, false);
				num++;
			}
			if (IdentityMapping.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				IdentityMapping.Tracer.TraceDebug<string>((long)this.GetHashCode(), "IdentityMapping.InitializeIfNeeded: performing lookup in AD for the following SMTP addresses: {0}", string.Join<SmtpProxyAddress>(",", array));
			}
			Result<ADRawEntry>[] array2 = this.recipientSession.FindByProxyAddresses(array, new PropertyDefinition[]
			{
				ADRecipientSchema.ExternalDirectoryObjectId
			});
			this.smtpAddressToIdentity = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i < array2.Length; i++)
			{
				string smtpAddress = array[i].SmtpAddress;
				Result<ADRawEntry> result = array2[i];
				if (result.Data == null)
				{
					LogWriter.TraceAndLog(new LogWriter.TraceMethod(IdentityMapping.Tracer.TraceError), 1, this.GetHashCode(), "IdentityMapping.InitializeIfNeeded: lookup object AD object SMTP addresses '{0}' failed due error: {1}", new object[]
					{
						smtpAddress,
						result.Error
					});
				}
				else
				{
					string text = result.Data[ADRecipientSchema.ExternalDirectoryObjectId] as string;
					Guid guid;
					if (text != null && Guid.TryParse(text, out guid))
					{
						this.smtpAddressToIdentity.Add(smtpAddress, guid);
						LogWriter.TraceAndLog(new LogWriter.TraceMethod(IdentityMapping.Tracer.TraceDebug), 4, this.GetHashCode(), "IdentityMapping.InitializeIfNeeded: AD object with SMTP addresses '{0}' maps to ExternalDirectoryObjectId '{1}'", new object[]
						{
							smtpAddress,
							guid
						});
					}
					else
					{
						LogWriter.TraceAndLog(new LogWriter.TraceMethod(IdentityMapping.Tracer.TraceError), 1, this.GetHashCode(), "IdentityMapping.InitializeIfNeeded: ExternalDirectoryObjectId is either empty or not valid guid for AD object with SMTP address: '{0}'", new object[]
						{
							smtpAddress
						});
					}
				}
			}
			this.smtpAddresses.Clear();
		}

		// Token: 0x040005BF RID: 1471
		private static readonly Trace Tracer = ExTraceGlobals.FederatedDirectoryTracer;

		// Token: 0x040005C0 RID: 1472
		private readonly IRecipientSession recipientSession;

		// Token: 0x040005C1 RID: 1473
		private readonly HashSet<string> smtpAddresses;

		// Token: 0x040005C2 RID: 1474
		private Dictionary<string, Guid> smtpAddressToIdentity;
	}
}
