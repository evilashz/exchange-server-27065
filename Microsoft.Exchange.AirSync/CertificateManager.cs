using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000040 RID: 64
	internal class CertificateManager
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x000160CC File Offset: 0x000142CC
		public CertificateManager(ExchangePrincipal exchangePrincipal, MailboxSession mailboxSession, int lcid, int certificateSlotsLeft, SmimeConfigurationContainer smimeConfiguration, OrganizationId organizationId)
		{
			this.certificateSlotsLeft = certificateSlotsLeft;
			this.mailboxSession = mailboxSession;
			this.lcid = lcid;
			this.exchangePrincipal = exchangePrincipal;
			this.smimeConfiguration = smimeConfiguration;
			this.organizationId = organizationId;
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00016104 File Offset: 0x00014304
		private IRecipientSession RecipientSession
		{
			get
			{
				if (this.recipientSession == null)
				{
					this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, null, this.lcid, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(Command.CurrentOrganizationId), 148, "RecipientSession", "f:\\15.00.1497\\sources\\dev\\AirSync\\src\\AirSync\\CertificateManager.cs");
				}
				return this.recipientSession;
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00016154 File Offset: 0x00014354
		internal static List<ChainValidityStatus> ValidateCertificates(List<string> trustedCertificateList, List<string> certificateChainList, List<string> certificateList, bool checkCRL, int hashCodeForTracing, MailboxLogger mailboxLogger, bool againstADConfiguration, string organizationId)
		{
			X509Store trustedStore = CertificateManager.AddChainCertsToStore(trustedCertificateList, hashCodeForTracing);
			X509Store chainBuildStore = CertificateManager.AddChainCertsToStore(certificateChainList, hashCodeForTracing);
			List<ChainValidityStatus> list = new List<ChainValidityStatus>(certificateList.Count);
			foreach (string text in certificateList)
			{
				ChainContext chainContext = null;
				try
				{
					X509Certificate2 certificate = new X509Certificate2(Convert.FromBase64String(text));
					ChainValidityStatus item = X509CertificateCollection.ValidateCertificate(certificate, null, X509KeyUsageFlags.NonRepudiation | X509KeyUsageFlags.DigitalSignature, checkCRL, trustedStore, chainBuildStore, ref chainContext, againstADConfiguration, organizationId);
					list.Add(item);
				}
				catch (CryptographicException ex)
				{
					if (mailboxLogger != null)
					{
						mailboxLogger.SetData(MailboxLogDataName.ValidateCertCommand_ProcessCommand_Per_Cert_Exception, ex.ToString());
					}
					AirSyncDiagnostics.TraceError<string, CryptographicException>(ExTraceGlobals.RequestTracer, null, "Failed to validate certificate: '{0}', Error: '{1}'", text, ex);
					list.Add((ChainValidityStatus)2148098052U);
				}
				finally
				{
					if (chainContext != null)
					{
						chainContext.Dispose();
					}
				}
			}
			return list;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000162B4 File Offset: 0x000144B4
		internal static SmimeConfigurationContainer LoadSmimeConfiguration(OrganizationId organizationId, int hashCodeForTracing)
		{
			SmimeConfigurationContainer config = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, sessionSettings, 265, "LoadSmimeConfiguration", "f:\\15.00.1497\\sources\\dev\\AirSync\\src\\AirSync\\CertificateManager.cs");
				SmimeConfigurationContainer[] array = tenantOrTopologyConfigurationSession.Find<SmimeConfigurationContainer>(SmimeConfigurationContainer.GetWellKnownParentLocation(tenantOrTopologyConfigurationSession.GetOrgContainerId()), QueryScope.SubTree, null, null, 1);
				if (array != null && array.Length > 0)
				{
					config = array[0];
				}
			});
			if (!adoperationResult.Succeeded || config == null)
			{
				AirSyncDiagnostics.TraceError<bool, OrganizationId, string>(ExTraceGlobals.RequestTracer, null, "Failed to find SmimeConfigurationContainer: Succeeded='{0}' OrganizationId='{1}', Exception='{2}'", adoperationResult.Succeeded, organizationId, (adoperationResult.Exception != null) ? adoperationResult.Exception.ToString() : "NULL");
			}
			return config;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00016334 File Offset: 0x00014534
		internal void GetRecipientCerts(AmbiguousRecipientToResolve recipient)
		{
			List<RecipientAddress> list = new List<RecipientAddress>(recipient.ResolvedTo.Count);
			int num = -1;
			foreach (ResolvedRecipient resolvedRecipient in recipient.ResolvedTo)
			{
				RecipientAddress resolvedTo = resolvedRecipient.ResolvedTo;
				num = (resolvedTo.Index = num + 1);
				switch (resolvedTo.AddressOrigin)
				{
				case AddressOrigin.Store:
					resolvedRecipient.CertificateRecipientCount++;
					this.GetContactCert(resolvedRecipient);
					break;
				case AddressOrigin.Directory:
					if (this.exchangePrincipal.MailboxInfo.IsAggregated)
					{
						resolvedRecipient.CertificateRecipientCount++;
					}
					else
					{
						list.Add(resolvedTo);
					}
					break;
				default:
					resolvedRecipient.CertificateRecipientCount++;
					break;
				}
			}
			if (list.Count > 0)
			{
				this.GetDirectoryCerts(list, recipient);
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00016428 File Offset: 0x00014628
		private static X509Store AddChainCertsToStore(List<string> certificateChainList, int hashCodeForTracing)
		{
			X509Store x509Store = CertificateStore.Open(StoreType.Memory, null, OpenFlags.ReadWrite);
			try
			{
				foreach (string s in certificateChainList)
				{
					X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
					x509Certificate2Collection.Import(Convert.FromBase64String(s));
					x509Store.AddRange(x509Certificate2Collection);
				}
			}
			catch (SecurityException arg)
			{
				AirSyncDiagnostics.TraceDebug<SecurityException>(ExTraceGlobals.RequestTracer, null, "Failed to add certificates to temporary memory store: '{0}'", arg);
			}
			catch (CryptographicException arg2)
			{
				AirSyncDiagnostics.TraceDebug<CryptographicException>(ExTraceGlobals.RequestTracer, null, "Failed to add certificates to temporary memory store: '{0}'", arg2);
			}
			return x509Store;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000164D8 File Offset: 0x000146D8
		private X509Certificate2 GetADRecipientCert(ADRawEntry adrecipient)
		{
			byte[][] array = CertificateManager.FindCertificatesForADRecipient(adrecipient);
			if (array.Length == 0)
			{
				return null;
			}
			ProxyAddressCollection proxyAddressCollection = adrecipient[ADRecipientSchema.EmailAddresses] as ProxyAddressCollection;
			List<string> list = new List<string>(proxyAddressCollection.Count);
			int num = 0;
			while (proxyAddressCollection != null && num < proxyAddressCollection.Count)
			{
				list.Add(proxyAddressCollection[num].AddressString);
				num++;
			}
			return this.FindBestCertificate(array, list, false);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00016540 File Offset: 0x00014740
		private X509Certificate2 FindBestCertificate(byte[][] certBlobs, IEnumerable<string> emails, bool iscontact)
		{
			if (certBlobs == null)
			{
				return null;
			}
			X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
			foreach (byte[] rawData in certBlobs)
			{
				if (iscontact)
				{
					x509CertificateCollection.ImportFromContact(rawData);
				}
				else
				{
					x509CertificateCollection.Import(rawData);
				}
			}
			X509Store x509Store = null;
			if (this.smimeConfiguration != null)
			{
				string text = this.smimeConfiguration.SMIMECertificateIssuingCAFull();
				if (!string.IsNullOrWhiteSpace(text))
				{
					x509Store = CertificateStore.Open(StoreType.Memory, null, OpenFlags.ReadWrite);
					X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
					x509Certificate2Collection.Import(Convert.FromBase64String(text));
					x509Store.AddRange(x509Certificate2Collection);
				}
			}
			return x509CertificateCollection.FindSMimeCertificate(emails, X509KeyUsageFlags.KeyEncipherment, false, x509Store, this.organizationId.ToString());
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000165E0 File Offset: 0x000147E0
		private static byte[][] FindCertificatesForADRecipient(ADRawEntry adrecipient)
		{
			if (adrecipient == null)
			{
				throw new ArgumentNullException("adRecipient");
			}
			byte[][] origin = CertificateManager.MultiValuePropertyToByteArray(adrecipient[ADRecipientSchema.Certificate] as MultiValuedProperty<byte[]>);
			byte[][] appendant = CertificateManager.MultiValuePropertyToByteArray(adrecipient[ADRecipientSchema.SMimeCertificate] as MultiValuedProperty<byte[]>);
			return CertificateManager.AppendArray(origin, appendant);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00016630 File Offset: 0x00014830
		private static bool IsADDistributionList(RecipientType recipientType)
		{
			return recipientType == RecipientType.Group || recipientType == RecipientType.MailUniversalDistributionGroup || recipientType == RecipientType.MailUniversalSecurityGroup || recipientType == RecipientType.MailNonUniversalGroup || recipientType == RecipientType.DynamicDistributionGroup;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001664C File Offset: 0x0001484C
		private static byte[][] AppendArray(byte[][] origin, byte[][] appendant)
		{
			if (origin == null)
			{
				return appendant;
			}
			if (appendant == null)
			{
				return origin;
			}
			byte[][] array = new byte[origin.Length + appendant.Length][];
			int i;
			for (i = 0; i < origin.Length; i++)
			{
				array[i] = origin[i];
			}
			int j = 0;
			while (j < appendant.Length)
			{
				array[i] = appendant[j];
				j++;
				i++;
			}
			return array;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000166A0 File Offset: 0x000148A0
		private static byte[][] MultiValuePropertyToByteArray(MultiValuedProperty<byte[]> property)
		{
			byte[][] array = null;
			if (property != null)
			{
				array = new byte[property.Count][];
				property.CopyTo(array, 0);
			}
			return array;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000166C8 File Offset: 0x000148C8
		private void GetContactCert(ResolvedRecipient recipient)
		{
			RecipientAddress resolvedTo = recipient.ResolvedTo;
			if (resolvedTo.StoreObjectId == null)
			{
				return;
			}
			List<PropertyDefinition> list = new List<PropertyDefinition>(4);
			list.Add(ContactSchema.UserX509Certificates);
			bool flag = string.Equals(resolvedTo.RoutingType, "EX", StringComparison.OrdinalIgnoreCase);
			if (flag)
			{
				list.AddRange(CertificateManager.contactEmailProperties);
			}
			try
			{
				using (Item item = Item.Bind(this.mailboxSession, resolvedTo.StoreObjectId, list.ToArray()))
				{
					List<string> list2 = new List<string>(4);
					list2.Add(resolvedTo.RoutingAddress);
					if (flag)
					{
						foreach (PropertyDefinition propertyDefinition in CertificateManager.contactEmailProperties)
						{
							Participant participant = item.TryGetProperty(propertyDefinition) as Participant;
							if (participant != null && string.Equals(participant.RoutingType, "EX", StringComparison.OrdinalIgnoreCase) && string.Equals(participant.EmailAddress, resolvedTo.RoutingAddress, StringComparison.OrdinalIgnoreCase))
							{
								list2.Add(participant.GetValueOrDefault<string>(ParticipantSchema.EmailAddressForDisplay));
								break;
							}
						}
					}
					X509Certificate2 x509Certificate = this.FindBestCertificate(item.TryGetProperty(ContactSchema.UserX509Certificates) as byte[][], list2, true);
					if (x509Certificate != null)
					{
						recipient.CertificateCount++;
						if (!recipient.GlobalCertLimitWasHit && this.certificateSlotsLeft <= 0)
						{
							recipient.GlobalCertLimitWasHit = true;
							this.certificateSlotsLeft += recipient.Certificates.Count;
							recipient.Certificates.Clear();
						}
						else if (!recipient.GlobalCertLimitWasHit)
						{
							recipient.Certificates.Add(x509Certificate);
							this.certificateSlotsLeft--;
						}
					}
				}
			}
			catch (ObjectNotFoundException)
			{
				AirSyncDiagnostics.TraceDebug<StoreObjectId>(ExTraceGlobals.RequestTracer, null, "Object Not Found while trying to retrieve the certificate: '{0}'", resolvedTo.StoreObjectId);
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000168A8 File Offset: 0x00014AA8
		private void GetDirectoryCerts(List<RecipientAddress> addressList, AmbiguousRecipientToResolve recipient)
		{
			List<string> list = new List<string>(addressList.Count);
			foreach (RecipientAddress recipientAddress in addressList)
			{
				list.Add(recipientAddress.RoutingAddress);
			}
			Result<ADRawEntry>[] array = this.RecipientSession.FindByLegacyExchangeDNs(list.ToArray(), CertificateManager.adrecipientProperties);
			if (Command.CurrentCommand != null)
			{
				Command.CurrentCommand.Context.ProtocolLogger.SetValue(ProtocolLoggerData.DomainController, this.recipientSession.LastUsedDc);
			}
			for (int i = 0; i < array.Length; i++)
			{
				Result<ADRawEntry> result = array[i];
				if (result.Data != null)
				{
					ResolvedRecipient resolvedRecipient = recipient.ResolvedTo[addressList[i].Index];
					if (CertificateManager.IsADDistributionList((RecipientType)result.Data[ADRecipientSchema.RecipientType]))
					{
						CertificateManager.ADDistributionListExpansion addistributionListExpansion = new CertificateManager.ADDistributionListExpansion(this, new ADRecipientExpansion(CertificateManager.adrecipientProperties, this.organizationId));
						addistributionListExpansion.Expand(result.Data);
						resolvedRecipient.CertificateRecipientCount += addistributionListExpansion.Size;
						resolvedRecipient.CertificateCount += addistributionListExpansion.Certificates.Count;
						if (!resolvedRecipient.GlobalCertLimitWasHit && resolvedRecipient.Certificates.Count + addistributionListExpansion.Certificates.Count <= this.certificateSlotsLeft)
						{
							resolvedRecipient.Certificates.AddRange(addistributionListExpansion.Certificates.ToArray());
							this.certificateSlotsLeft -= addistributionListExpansion.Certificates.Count;
						}
						else
						{
							resolvedRecipient.GlobalCertLimitWasHit = true;
							this.certificateSlotsLeft += resolvedRecipient.Certificates.Count;
							resolvedRecipient.Certificates.Clear();
						}
					}
					else
					{
						resolvedRecipient.CertificateRecipientCount++;
						X509Certificate2 adrecipientCert = this.GetADRecipientCert(result.Data);
						if (adrecipientCert != null)
						{
							resolvedRecipient.CertificateCount++;
							if (!resolvedRecipient.GlobalCertLimitWasHit && this.certificateSlotsLeft <= 0)
							{
								resolvedRecipient.GlobalCertLimitWasHit = true;
								this.certificateSlotsLeft += resolvedRecipient.Certificates.Count;
								resolvedRecipient.Certificates.Clear();
							}
							else if (!resolvedRecipient.GlobalCertLimitWasHit)
							{
								resolvedRecipient.Certificates.Add(adrecipientCert);
								this.certificateSlotsLeft--;
							}
						}
					}
				}
			}
		}

		// Token: 0x040002CA RID: 714
		private static readonly PropertyDefinition[] adrecipientProperties = new PropertyDefinition[]
		{
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.LegacyExchangeDN,
			ADObjectSchema.Id,
			ADRecipientSchema.Certificate,
			ADRecipientSchema.SMimeCertificate,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.EmailAddresses
		};

		// Token: 0x040002CB RID: 715
		private static readonly PropertyDefinition[] contactEmailProperties = new PropertyDefinition[]
		{
			ContactSchema.Email1,
			ContactSchema.Email2,
			ContactSchema.Email3
		};

		// Token: 0x040002CC RID: 716
		private ExchangePrincipal exchangePrincipal;

		// Token: 0x040002CD RID: 717
		private int lcid;

		// Token: 0x040002CE RID: 718
		private MailboxSession mailboxSession;

		// Token: 0x040002CF RID: 719
		private int certificateSlotsLeft;

		// Token: 0x040002D0 RID: 720
		private IRecipientSession recipientSession;

		// Token: 0x040002D1 RID: 721
		private SmimeConfigurationContainer smimeConfiguration;

		// Token: 0x040002D2 RID: 722
		private OrganizationId organizationId;

		// Token: 0x02000041 RID: 65
		private class ADDistributionListExpansion
		{
			// Token: 0x060003E8 RID: 1000 RVA: 0x00016BA7 File Offset: 0x00014DA7
			public ADDistributionListExpansion(CertificateManager manager, ADRecipientExpansion adrecipientExpansion)
			{
				this.manager = manager;
				this.adrecipientExpansion = adrecipientExpansion;
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00016BD3 File Offset: 0x00014DD3
			public List<X509Certificate2> Certificates
			{
				get
				{
					return this.certificates;
				}
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x060003EA RID: 1002 RVA: 0x00016BDB File Offset: 0x00014DDB
			public int Size
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x060003EB RID: 1003 RVA: 0x00016BE3 File Offset: 0x00014DE3
			public void Expand(ADRawEntry recipient)
			{
				if ((bool)recipient[ADGroupSchema.HiddenGroupMembershipEnabled])
				{
					return;
				}
				this.adrecipientExpansion.Expand(recipient, new ADRecipientExpansion.HandleRecipientDelegate(this.OnRecipient), new ADRecipientExpansion.HandleFailureDelegate(this.OnFailure));
			}

			// Token: 0x060003EC RID: 1004 RVA: 0x00016C1C File Offset: 0x00014E1C
			private ExpansionControl OnFailure(ExpansionFailure failure, ADRawEntry recipient, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentExpansionType)
			{
				return ExpansionControl.Continue;
			}

			// Token: 0x060003ED RID: 1005 RVA: 0x00016C20 File Offset: 0x00014E20
			private ExpansionControl OnRecipient(ADRawEntry recipient, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentExpansionType)
			{
				bool flag = CertificateManager.IsADDistributionList((RecipientType)recipient[ADRecipientSchema.RecipientType]);
				if (flag && (bool)recipient[ADGroupSchema.HiddenGroupMembershipEnabled])
				{
					this.certificates.Clear();
					this.size = 0;
					return ExpansionControl.Terminate;
				}
				if (!flag && !this.previouslySeen.Contains(recipient.Identity.ToString()))
				{
					this.size++;
					this.previouslySeen.Add(recipient.Identity.ToString());
					X509Certificate2 adrecipientCert = this.manager.GetADRecipientCert(recipient);
					if (adrecipientCert != null)
					{
						this.certificates.Add(adrecipientCert);
					}
				}
				if (this.size > GlobalSettings.MaxSMimeADDistributionListExpansion)
				{
					this.size = 0;
					this.certificates.Clear();
					return ExpansionControl.Terminate;
				}
				return ExpansionControl.Continue;
			}

			// Token: 0x040002D3 RID: 723
			private ADRecipientExpansion adrecipientExpansion;

			// Token: 0x040002D4 RID: 724
			private List<X509Certificate2> certificates = new List<X509Certificate2>();

			// Token: 0x040002D5 RID: 725
			private HashSet<string> previouslySeen = new HashSet<string>();

			// Token: 0x040002D6 RID: 726
			private int size;

			// Token: 0x040002D7 RID: 727
			private CertificateManager manager;
		}
	}
}
