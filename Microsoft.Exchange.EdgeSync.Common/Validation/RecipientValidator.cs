using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Transport.RecipientAPI;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x02000049 RID: 73
	internal class RecipientValidator : ConfigValidator
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x0000924C File Offset: 0x0000744C
		public RecipientValidator(ReplicationTopology topology) : base(topology, "Recipients")
		{
			base.ConfigDirectoryPath = string.Empty;
			base.LdapQuery = Schema.Query.QueryAllSmtpRecipients;
			this.orgConfigRoot = base.OrgAdRootPath;
			base.OrgAdRootPath = DistinguishedName.RemoveLeafRelativeDistinguishedNames(base.Topology.LocalHub.DistinguishedName, 8);
			this.compareAttributes = this.PayloadAttributes;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000092BA File Offset: 0x000074BA
		protected override IDirectorySession DataSession
		{
			get
			{
				return base.Topology.RecipientSession;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000092C8 File Offset: 0x000074C8
		protected override string[] PayloadAttributes
		{
			get
			{
				if (this.payloadAttributesArray == null)
				{
					List<string> list = new List<string>(RecipientSchema.AttributeNames.Length - 1);
					foreach (string text in RecipientSchema.AttributeNames)
					{
						if (!text.Equals("msExchVersion", StringComparison.OrdinalIgnoreCase))
						{
							list.Add(text);
						}
					}
					this.payloadAttributesArray = list.ToArray();
				}
				return this.payloadAttributesArray;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000932B File Offset: 0x0000752B
		protected override string[] ReadAttributes
		{
			get
			{
				return RecipientValidator.readAttributes;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00009332 File Offset: 0x00007532
		protected override string ADSearchPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00009339 File Offset: 0x00007539
		protected override string ADAMSearchPath
		{
			get
			{
				return "CN=Recipients,OU=MSExchangeGateway";
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00009340 File Offset: 0x00007540
		protected override string ADAMLdapQuery
		{
			get
			{
				return "(proxyAddresses=*)";
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00009348 File Offset: 0x00007548
		public override EdgeConfigStatus Validate(EdgeConnectionInfo subscription)
		{
			EdgeConfigStatus edgeConfigStatus = base.Validate(subscription);
			this.ValidateExchangeServerRecipient(edgeConfigStatus);
			return edgeConfigStatus;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009368 File Offset: 0x00007568
		public EdgeConfigStatus ValidateOneRecipient(EdgeConnectionInfo subscription, string proxyAddressToVerify)
		{
			ADObjectId adobjectId = null;
			string[] attributeNames = RecipientSchema.AttributeNames;
			string[] array = RecipientValidator.readAttributes;
			int num = attributeNames.Length;
			int num2 = array.Length;
			string[] array2 = new string[num + num2];
			attributeNames.CopyTo(array2, 0);
			array.CopyTo(array2, num);
			Connection connection = null;
			EdgeConfigStatus result;
			try
			{
				connection = new Connection(this.DataSession.GetReadConnection(null, ref adobjectId));
				List<ExSearchResultEntry> list = new List<ExSearchResultEntry>();
				foreach (ExSearchResultEntry item in connection.PagedScan(null, "(proxyAddresses=" + proxyAddressToVerify + ")", SearchScope.Subtree, array2))
				{
					list.Add(item);
				}
				if (list.Count == 0)
				{
					result = new RecipientConfigStatus(SyncStatus.NotSynchronized, "Recipient doesn't exist in source Active Directory");
				}
				else if (list.Count > 1)
				{
					RecipientConfigStatus recipientConfigStatus = new RecipientConfigStatus(SyncStatus.NotSynchronized, "More than one recipient found in source Active Directory and may cause NDR on Edge server. RecipientStatus.ConflictObjects contains relevant entries.");
					foreach (ExSearchResultEntry exSearchResultEntry in list)
					{
						recipientConfigStatus.ConflictObjects.Add(new ADObjectId(exSearchResultEntry.DistinguishedName));
					}
					result = recipientConfigStatus;
				}
				else
				{
					string hashedFormWithPrefix = this.proxyAddressHasher.GetHashedFormWithPrefix(proxyAddressToVerify.Substring(5));
					List<ExSearchResultEntry> list2 = new List<ExSearchResultEntry>();
					foreach (ExSearchResultEntry item2 in subscription.EdgeConnection.PagedScan(this.ADAMSearchPath, "(proxyAddresses=" + hashedFormWithPrefix + ")", SearchScope.Subtree, array2))
					{
						list2.Add(item2);
					}
					if (list2.Count > 1)
					{
						RecipientConfigStatus recipientConfigStatus2 = new RecipientConfigStatus(SyncStatus.NotSynchronized, "More than one recipient found in target Edge Server and may cause NDR on Edge server. RecipientStatus.ConflictObjects contains relevant entries.");
						foreach (ExSearchResultEntry exSearchResultEntry2 in list2)
						{
							recipientConfigStatus2.ConflictObjects.Add(new ADObjectId(exSearchResultEntry2.DistinguishedName));
						}
						result = recipientConfigStatus2;
					}
					else
					{
						ExSearchResultEntry exSearchResultEntry3 = list[0];
						DirectoryAttribute directoryAttribute = exSearchResultEntry3.Attributes["objectGUID"];
						Guid guid = new Guid((byte[])directoryAttribute.GetValues(typeof(byte[]))[0]);
						string absolutePath = "cn=" + guid.ToString() + "," + this.ADAMSearchPath;
						ExSearchResultEntry exSearchResultEntry4 = subscription.EdgeConnection.ReadObjectEntry(absolutePath, array2);
						if (exSearchResultEntry4 == null)
						{
							result = new RecipientConfigStatus(SyncStatus.NotSynchronized, "Recipient doesn't exist in target Edge Server and may cause NDR on Edge server")
							{
								OrgOnlyObjects = 
								{
									new ADObjectId(exSearchResultEntry3.DistinguishedName)
								}
							};
						}
						else if (!this.CompareAttributes(exSearchResultEntry4, exSearchResultEntry3, attributeNames))
						{
							result = new RecipientConfigStatus(SyncStatus.NotSynchronized, "Recipient exists in target Edge Server but attributes are not synchronized")
							{
								ConflictObjects = 
								{
									new ADObjectId(exSearchResultEntry3.DistinguishedName)
								}
							};
						}
						else
						{
							DirectoryAttribute directoryAttribute2 = null;
							bool flag = false;
							if (exSearchResultEntry4.Attributes.TryGetValue("msExchRequireAuthToSendTo", out directoryAttribute2) && directoryAttribute2 != null && directoryAttribute2.Count > 0 && bool.TryParse((string)directoryAttribute2[0], out flag) && flag)
							{
								result = new RecipientConfigStatus(SyncStatus.Synchronized, "Recipient requires sender authentication and this may cause NDR on Edge server. RecipientStatus.ConflictObjects contains the recipient object in source Active Directory")
								{
									ConflictObjects = 
									{
										new ADObjectId(exSearchResultEntry3.DistinguishedName)
									}
								};
							}
							else
							{
								result = new RecipientConfigStatus(SyncStatus.Synchronized, null);
							}
						}
					}
				}
			}
			catch (ExDirectoryException ex)
			{
				result = new RecipientConfigStatus(SyncStatus.DirectoryError, ex.Message);
			}
			catch (ADTransientException ex2)
			{
				result = new RecipientConfigStatus(SyncStatus.DirectoryError, ex2.Message);
			}
			catch (ADOperationException ex3)
			{
				result = new RecipientConfigStatus(SyncStatus.DirectoryError, ex3.Message);
			}
			finally
			{
				if (connection != null)
				{
					connection.Dispose();
					connection = null;
				}
			}
			return result;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000097D4 File Offset: 0x000079D4
		protected override string GetADRelativePath(ExSearchResultEntry searchEntry)
		{
			Guid guid = new Guid((byte[])searchEntry.Attributes["objectGUID"][0]);
			return string.Format("cn={0}", guid.ToString().ToLower());
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000981F File Offset: 0x00007A1F
		protected override bool CompareAttributes(ExSearchResultEntry edgeEntry, ExSearchResultEntry hubEntry, string[] copyAttributes)
		{
			this.HashProxyAddresses(hubEntry);
			return base.CompareAttributes(edgeEntry, hubEntry, this.compareAttributes);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009838 File Offset: 0x00007A38
		private void HashProxyAddresses(ExSearchResultEntry sourceEntry)
		{
			DirectoryAttribute directoryAttribute = sourceEntry.Attributes["proxyAddresses"];
			List<string> list = new List<string>();
			for (int i = 0; i < directoryAttribute.Count; i++)
			{
				string text = directoryAttribute[i] as string;
				if (text.StartsWith("sh:", StringComparison.OrdinalIgnoreCase))
				{
					return;
				}
				if (text.StartsWith("smtp:", StringComparison.OrdinalIgnoreCase))
				{
					string smtpAddress = text.Substring(5);
					string hashedFormWithPrefix = this.proxyAddressHasher.GetHashedFormWithPrefix(smtpAddress);
					list.Add(hashedFormWithPrefix);
				}
			}
			DirectoryAttribute value = new DirectoryAttribute(directoryAttribute.Name, list.ToArray());
			sourceEntry.Attributes["proxyAddresses"] = value;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000098DC File Offset: 0x00007ADC
		private void ValidateExchangeServerRecipient(EdgeConfigStatus status)
		{
			Connection connection = null;
			if (status.SyncStatus != SyncStatus.NotSynchronized && status.SyncStatus != SyncStatus.Synchronized)
			{
				return;
			}
			try
			{
				ADObjectId adobjectId = null;
				string absolutePath = "CN=" + ADMicrosoftExchangeRecipient.DefaultName + ",CN=Transport Settings," + this.orgConfigRoot;
				string absolutePath2 = "CN=" + ADMicrosoftExchangeRecipient.DefaultName + ",CN=Transport Settings," + base.AdamRootPath;
				connection = new Connection(this.DataSession.GetReadConnection(null, ref adobjectId));
				ExSearchResultEntry exSearchResultEntry = connection.ReadObjectEntry(absolutePath, Schema.ExchangeRecipient.PayloadAttributes);
				ExSearchResultEntry exSearchResultEntry2 = base.CurrentEdgeConnection.EdgeConnection.ReadObjectEntry(absolutePath2, Schema.ExchangeRecipient.PayloadAttributes);
				if (exSearchResultEntry != exSearchResultEntry2)
				{
					if (exSearchResultEntry == null)
					{
						status.SyncStatus = SyncStatus.NotSynchronized;
						if (base.MaxReportedLength.IsUnlimited || (ulong)base.MaxReportedLength.Value > (ulong)((long)status.EdgeOnlyObjects.Count))
						{
							status.EdgeOnlyObjects.Add(new ADObjectId(exSearchResultEntry2.DistinguishedName));
						}
					}
					else if (exSearchResultEntry2 == null)
					{
						status.SyncStatus = SyncStatus.NotSynchronized;
						if (base.MaxReportedLength.IsUnlimited || (ulong)base.MaxReportedLength.Value > (ulong)((long)status.OrgOnlyObjects.Count))
						{
							status.OrgOnlyObjects.Add(new ADObjectId(exSearchResultEntry.DistinguishedName));
						}
					}
					else
					{
						if (!base.CompareAttributes(exSearchResultEntry2, exSearchResultEntry, Schema.ExchangeRecipient.PayloadAttributes))
						{
							status.SyncStatus = SyncStatus.NotSynchronized;
							if (base.MaxReportedLength.IsUnlimited || (ulong)base.MaxReportedLength.Value > (ulong)((long)status.ConflictObjects.Count))
							{
								status.ConflictObjects.Add(new ADObjectId(exSearchResultEntry.DistinguishedName));
							}
						}
						if (status.SyncStatus == SyncStatus.Synchronized)
						{
							status.SynchronizedObjects += 1U;
						}
					}
				}
			}
			catch (ExDirectoryException)
			{
				status.SyncStatus = SyncStatus.DirectoryError;
			}
			finally
			{
				if (connection != null)
				{
					connection.Dispose();
					connection = null;
				}
			}
		}

		// Token: 0x04000146 RID: 326
		private static readonly string[] readAttributes = new string[]
		{
			"objectGUID"
		};

		// Token: 0x04000147 RID: 327
		private readonly ProxyAddressHasher proxyAddressHasher = new ProxyAddressHasher();

		// Token: 0x04000148 RID: 328
		private string[] compareAttributes;

		// Token: 0x04000149 RID: 329
		private string orgConfigRoot;

		// Token: 0x0400014A RID: 330
		private string[] payloadAttributesArray;
	}
}
