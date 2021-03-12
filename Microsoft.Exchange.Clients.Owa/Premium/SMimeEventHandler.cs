using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004D9 RID: 1241
	[OwaEventSegmentation(Feature.SMime)]
	[OwaEventNamespace("SMime")]
	internal sealed class SMimeEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002F32 RID: 12082 RVA: 0x00110696 File Offset: 0x0010E896
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(SMimeEventHandler));
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x001106A7 File Offset: 0x0010E8A7
		[OwaEvent("GetCerts")]
		[OwaEventParameter("addrs", typeof(RecipientInfo), true)]
		public void GetCerts()
		{
			this.getCertificateResponseWriter = new SMimeEventHandler.GetCertificateListResponseWriter(base.UserContext, this.Writer);
			this.GetCertificates();
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x001106C6 File Offset: 0x0010E8C6
		[OwaEventParameter("addrs", typeof(RecipientInfo), true)]
		[OwaEvent("GetCertsInfo")]
		public void GetCertsInfo()
		{
			this.getCertificateResponseWriter = new SMimeEventHandler.GetCertificateInformationResponseWriter(base.UserContext, this.Writer);
			this.GetCertificates();
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x001106E8 File Offset: 0x0010E8E8
		[OwaEventParameter("IsVCard", typeof(int))]
		[OwaEvent("UploadEmbeddedItem")]
		[OwaEventParameter("MimeBlob", typeof(string))]
		[OwaEventParameter("VCardCharset", typeof(string), false, true)]
		public void UploadEmbeddedItem()
		{
			bool flag = (int)base.GetParameter("IsVCard") == 1;
			string text = (string)base.GetParameter("VCardCharset");
			Encoding encoding = null;
			if (flag)
			{
				if (string.IsNullOrEmpty(text))
				{
					throw new OwaInvalidRequestException("VCardCharset must be set when IsVCard parameter is true.");
				}
				try
				{
					encoding = Encoding.GetEncoding(text);
				}
				catch (ArgumentException)
				{
					throw new OwaInvalidRequestException("VCardCharset is invalid.");
				}
			}
			using (MemoryStream memoryStream = new MemoryStream(Encoding.GetEncoding("windows-1252").GetBytes((string)base.GetParameter("MimeBlob"))))
			{
				using (Item item = this.CreateItemFromMime(memoryStream, base.UserContext.GetDeletedItemsFolderId(base.UserContext.MailboxSession).StoreObjectId, flag, encoding))
				{
					string className = item.ClassName;
					StoreObjectId objectId = item.Id.ObjectId;
					bool flag2 = false;
					if (ObjectClass.IsTask(className))
					{
						item.Load(new PropertyDefinition[]
						{
							TaskSchema.TaskType
						});
						flag2 = TaskUtilities.IsAssignedTaskType(TaskUtilities.GetTaskType(item));
					}
					if (ObjectClass.IsContact(className) || ObjectClass.IsDistributionList(className) || (ObjectClass.IsTask(className) && !flag2))
					{
						this.Writer.Write("?ae=PreFormAction&a=OpenEmbedded&t=");
					}
					else
					{
						this.Writer.Write("?ae=Item&t=");
					}
					this.Writer.Write(Utilities.UrlEncode(className));
					this.Writer.Write("&id=");
					this.Writer.Write(Utilities.UrlEncode(objectId.ToBase64String()));
					this.Writer.Write("&smemb=1&exdltdrft=1");
				}
			}
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x001108CC File Offset: 0x0010EACC
		private static string GetIdFromCertificate(X509Certificate2 certificate)
		{
			string emailAdress = X509PartialCertificate.GetEmailAdress(certificate);
			if (!string.IsNullOrEmpty(emailAdress))
			{
				return emailAdress;
			}
			string senderCertificateAttributesToDisplay = OwaRegistryKeys.SenderCertificateAttributesToDisplay;
			if (string.IsNullOrEmpty(senderCertificateAttributesToDisplay))
			{
				return null;
			}
			IList<KeyValuePair<Oid, string>> list = X500DistinguishedNameDecoder.Decode(certificate.SubjectName);
			if (list == null || list.Count == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			string[] array = OwaRegistryKeys.SenderCertificateAttributesToDisplay.Split(SMimeEventHandler.comma, StringSplitOptions.RemoveEmptyEntries);
			bool flag = true;
			bool flag2 = false;
			foreach (string text in array)
			{
				string text2 = text.Trim();
				if (!string.IsNullOrEmpty(text2))
				{
					Oid oid = new Oid(text2);
					flag2 = false;
					foreach (KeyValuePair<Oid, string> keyValuePair in list)
					{
						if (string.Equals(keyValuePair.Key.Value, oid.Value, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(keyValuePair.Value))
						{
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder();
							}
							if (!flag)
							{
								stringBuilder.Append(", ");
							}
							else
							{
								flag = false;
							}
							stringBuilder.Append(keyValuePair.Value);
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						return null;
					}
				}
			}
			if (stringBuilder == null)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x00110A20 File Offset: 0x0010EC20
		private static string GetIssuerDisplayNameFromCertificate(X509Certificate2 certificate)
		{
			IList<KeyValuePair<Oid, string>> list = X500DistinguishedNameDecoder.Decode(certificate.IssuerName);
			if (list == null || list.Count == 0)
			{
				return string.Empty;
			}
			foreach (KeyValuePair<Oid, string> keyValuePair in list)
			{
				if (string.Equals(keyValuePair.Key.Value, SMimeEventHandler.commonNameOid.Value, StringComparison.OrdinalIgnoreCase))
				{
					return keyValuePair.Value;
				}
			}
			return string.Empty;
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x00110AB0 File Offset: 0x0010ECB0
		private static void SafeAddADRecipient(HashSet<Participant> list, Participant participant)
		{
			bool flag = false;
			foreach (Participant participant2 in list)
			{
				if (string.Equals(participant.EmailAddress, participant2.EmailAddress, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				list.TryAdd(participant);
			}
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x00110B1C File Offset: 0x0010ED1C
		private Item CreateItemFromMime(MemoryStream mimeStream, StoreObjectId folderId, bool createContactFromVcard, Encoding encoding)
		{
			Item item;
			if (createContactFromVcard)
			{
				item = Contact.Create(base.UserContext.MailboxSession, folderId);
			}
			else
			{
				item = MessageItem.Create(base.UserContext.MailboxSession, folderId);
			}
			item[MessageItemSchema.IsDraft] = false;
			item.Load(new PropertyDefinition[]
			{
				MessageItemSchema.ExpiryTime
			});
			InboundConversionOptions options = Utilities.CreateInboundConversionOptions(base.UserContext);
			if (createContactFromVcard)
			{
				Contact.ImportVCard(item as Contact, mimeStream, encoding, options);
			}
			else
			{
				ItemConversion.ConvertAnyMimeToItem(item, mimeStream, options);
			}
			Utilities.SaveItem(item, false);
			return item;
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x00110BAC File Offset: 0x0010EDAC
		private void GetCertificates()
		{
			this.expansionTimeoutTime = new ExDateTime?(ExDateTime.UtcNow.AddMilliseconds((double)OwaRegistryKeys.DLExpansionTimeout));
			try
			{
				this.GetRecipientsCerts();
			}
			catch (SMimeEventHandler.SMimeEventHandlerException e)
			{
				this.getCertificateResponseWriter.RenderError(e);
				return;
			}
			this.RenderPdlCertMissingInfomation();
			this.getCertificateResponseWriter.RenderCertificates(this.certificateDictionary);
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x00110C18 File Offset: 0x0010EE18
		private void RenderPdlCertMissingInfomation()
		{
			if (this.pdlExpansionResults.Count == 0)
			{
				return;
			}
			foreach (KeyValuePair<Participant, HashSet<Participant>> keyValuePair in this.pdlExpansionResults)
			{
				int num = 0;
				int num2 = 0;
				foreach (Participant participant in keyValuePair.Value)
				{
					SMimeEventHandler.CertificatsInformation certificatsInformation;
					if (this.certificateDictionary.TryGetValue(participant, out certificatsInformation) || (!(participant.Origin is StoreParticipantOrigin) && string.Equals(participant.RoutingType, "EX", StringComparison.OrdinalIgnoreCase) && this.FindCertificateByRoutingAddress(participant.EmailAddress, out certificatsInformation)))
					{
						num += certificatsInformation.Total;
						num2 += certificatsInformation.Valid;
					}
					else
					{
						num++;
					}
				}
				if (num != num2)
				{
					this.getCertificateResponseWriter.RenderMissing(keyValuePair.Key.DisplayName, "IPM.DistList", ((StoreParticipantOrigin)keyValuePair.Key.Origin).OriginItemId.ToBase64String(), null, num, num2);
				}
			}
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x00110D54 File Offset: 0x0010EF54
		private void GetRecipientsCerts()
		{
			this.allRecipients = this.GroupRecipients();
			HashSet<Participant> hashSet = null;
			HashSet<Participant> hashSet2 = null;
			if (this.allRecipients[SMimeEventHandler.RecipientGroup.PDLs].Count > 0)
			{
				this.ExpandPdlIntoMembers(ref hashSet, ref hashSet2);
			}
			if (hashSet2 != null || this.allRecipients[SMimeEventHandler.RecipientGroup.Contacts].Count > 0)
			{
				this.GetStoreCerts(hashSet2 ?? this.allRecipients[SMimeEventHandler.RecipientGroup.Contacts]);
			}
			if (hashSet != null || this.allRecipients[SMimeEventHandler.RecipientGroup.AdRecipients].Count > 0)
			{
				this.GetDirectoryCerts(hashSet ?? this.allRecipients[SMimeEventHandler.RecipientGroup.AdRecipients]);
			}
			if (this.allRecipients[SMimeEventHandler.RecipientGroup.Others].Count > 0)
			{
				foreach (Participant participant in this.allRecipients[SMimeEventHandler.RecipientGroup.Others])
				{
					this.getCertificateResponseWriter.RenderMissing(participant.DisplayName);
				}
			}
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x00110E58 File Offset: 0x0010F058
		private void ExpandPdlIntoMembers(ref HashSet<Participant> adRecipientsAfterPdlExpansion, ref HashSet<Participant> contactsAfterPdlExpansion)
		{
			this.Writer.Write("<div id=pdl>");
			foreach (Participant participant in this.allRecipients[SMimeEventHandler.RecipientGroup.PDLs])
			{
				StoreObjectId originItemId = (participant.Origin as StoreParticipantOrigin).OriginItemId;
				Participant[] array = DistributionList.ExpandDeep(base.UserContext.MailboxSession, originItemId, false);
				if (array != null && array.Length != 0)
				{
					HashSet<Participant> hashSet = new HashSet<Participant>(array.Length);
					foreach (Participant item in array)
					{
						hashSet.TryAdd(item);
					}
					this.pdlExpansionResults.Add(participant, hashSet);
					this.Writer.Write("<div _id=\"");
					Utilities.HtmlEncode(originItemId.ToBase64String(), this.Writer);
					this.Writer.Write("\">");
					foreach (Participant participant2 in hashSet)
					{
						string text = participant2.TryGetProperty(ParticipantSchema.SmtpAddress) as string;
						if (string.IsNullOrEmpty(text))
						{
							text = ImceaAddress.Encode(participant2.RoutingType, participant2.EmailAddress, OwaConfigurationManager.Configuration.DefaultAcceptedDomain.DomainName.ToString());
						}
						this.Writer.Write("<p _sa=\"");
						Utilities.HtmlEncode(text, this.Writer);
						this.Writer.Write("\" _dn=\"");
						Utilities.HtmlEncode(participant2.DisplayName, this.Writer);
						StoreParticipantOrigin storeParticipantOrigin = participant2.Origin as StoreParticipantOrigin;
						if (storeParticipantOrigin != null)
						{
							if (contactsAfterPdlExpansion == null)
							{
								contactsAfterPdlExpansion = new HashSet<Participant>(this.allRecipients[SMimeEventHandler.RecipientGroup.Contacts]);
							}
							contactsAfterPdlExpansion.TryAdd(participant2);
							this.Writer.Write("\" _id=\"");
							Utilities.HtmlEncode(storeParticipantOrigin.OriginItemId.ToBase64String(), this.Writer);
						}
						else if (string.Equals(participant2.RoutingType, "EX", StringComparison.OrdinalIgnoreCase))
						{
							if (adRecipientsAfterPdlExpansion == null)
							{
								adRecipientsAfterPdlExpansion = new HashSet<Participant>(this.allRecipients[SMimeEventHandler.RecipientGroup.AdRecipients]);
							}
							SMimeEventHandler.SafeAddADRecipient(adRecipientsAfterPdlExpansion, participant2);
							this.Writer.Write("\" _em=\"");
							Utilities.HtmlEncode(participant2.EmailAddress, this.Writer);
						}
						this.Writer.Write("\"></p>");
					}
					this.Writer.Write("</div>");
				}
			}
			this.Writer.Write("</div>");
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x00111120 File Offset: 0x0010F320
		private Dictionary<SMimeEventHandler.RecipientGroup, HashSet<Participant>> GroupRecipients()
		{
			RecipientInfo[] array = (RecipientInfo[])base.GetParameter("addrs");
			Dictionary<SMimeEventHandler.RecipientGroup, HashSet<Participant>> dictionary = new Dictionary<SMimeEventHandler.RecipientGroup, HashSet<Participant>>(4);
			SMimeEventHandler.RecipientGroup[] array2 = new SMimeEventHandler.RecipientGroup[]
			{
				SMimeEventHandler.RecipientGroup.PDLs,
				SMimeEventHandler.RecipientGroup.Contacts,
				SMimeEventHandler.RecipientGroup.AdRecipients,
				SMimeEventHandler.RecipientGroup.Others
			};
			foreach (SMimeEventHandler.RecipientGroup key in array2)
			{
				dictionary[key] = new HashSet<Participant>();
			}
			for (int j = 0; j < array.Length; j++)
			{
				bool flag = false;
				for (int k = 0; k < j; k++)
				{
					if (array[k] == array[j] || (array[k].AddressOrigin == AddressOrigin.Directory && array[j].AddressOrigin == AddressOrigin.Directory && string.Equals(array[k].RoutingAddress, array[j].RoutingAddress, StringComparison.OrdinalIgnoreCase)))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					Participant participant;
					array[j].ToParticipant(out participant);
					if (Utilities.IsMapiPDL(participant.RoutingType) && participant.Origin is StoreParticipantOrigin)
					{
						dictionary[SMimeEventHandler.RecipientGroup.PDLs].TryAdd(participant);
					}
					else if (participant.Origin is StoreParticipantOrigin)
					{
						dictionary[SMimeEventHandler.RecipientGroup.Contacts].TryAdd(participant);
					}
					else if (participant.Origin is DirectoryParticipantOrigin)
					{
						dictionary[SMimeEventHandler.RecipientGroup.AdRecipients].TryAdd(participant.ChangeOrigin(new OneOffParticipantOrigin()));
					}
					else
					{
						dictionary[SMimeEventHandler.RecipientGroup.Others].TryAdd(participant);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x0011128C File Offset: 0x0010F48C
		private void AddCerts(Participant participant, List<X509Certificate2> certificates, int total, int valid)
		{
			this.certificateDictionary[participant] = new SMimeEventHandler.CertificatsInformation(certificates, total, valid);
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x001112A4 File Offset: 0x0010F4A4
		private bool AddCert(Participant participant, X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				return false;
			}
			this.AddCerts(participant, new List<X509Certificate2>(1)
			{
				certificate
			}, 1, 1);
			return true;
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x001112D0 File Offset: 0x0010F4D0
		private X509Certificate2 GetContactCertificate(StoreObjectId id)
		{
			try
			{
				using (Item item = Utilities.GetItem<Item>(base.UserContext, OwaStoreObjectId.CreateFromStoreObjectId(id, null), new PropertyDefinition[]
				{
					ContactSchema.UserX509Certificates
				}))
				{
					return Utilities.FindBestCertificate(ItemUtility.GetProperty<byte[][]>(item, ContactSchema.UserX509Certificates, null), null, true, false);
				}
			}
			catch (ObjectNotFoundException)
			{
			}
			return null;
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x00111344 File Offset: 0x0010F544
		private void GetStoreCerts(HashSet<Participant> contacts)
		{
			foreach (Participant participant in contacts)
			{
				if (!this.AddCert(participant, this.GetContactCertificate(((StoreParticipantOrigin)participant.Origin).OriginItemId)) && this.allRecipients[SMimeEventHandler.RecipientGroup.Contacts].Contains(participant))
				{
					this.getCertificateResponseWriter.RenderMissing(ThemeFileId.Contact, participant.DisplayName, "IPM.Contact", ((StoreParticipantOrigin)participant.Origin).OriginItemId.ToBase64String());
				}
			}
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x001113EC File Offset: 0x0010F5EC
		private void GetDirectoryCerts(HashSet<Participant> recipients)
		{
			HashSet<Participant> hashSet = new HashSet<Participant>(recipients.Count);
			List<string> list = new List<string>(recipients.Count);
			foreach (Participant participant in recipients)
			{
				list.Add(participant.EmailAddress);
			}
			Result<ADRawEntry>[] array = this.GetADRecipientSession().FindByLegacyExchangeDNs(list.ToArray(), SMimeEventHandler.adRecipientProperties);
			foreach (Result<ADRawEntry> result in array)
			{
				if (result.Data != null)
				{
					string text = result.Data[ADRecipientSchema.LegacyExchangeDN] as string;
					string legacyExchangeDN = base.UserContext.MailboxIdentity.GetOWAMiniRecipient().LegacyExchangeDN;
					bool checkRevocation = OwaRegistryKeys.CheckCRLOnSend && string.Equals(text, legacyExchangeDN, StringComparison.OrdinalIgnoreCase);
					foreach (Participant participant2 in recipients)
					{
						if (string.Equals(participant2.EmailAddress, text, StringComparison.OrdinalIgnoreCase))
						{
							hashSet.TryAdd(participant2);
							if (Utilities.IsADDistributionList((RecipientType)result.Data[ADRecipientSchema.RecipientType]))
							{
								if (this.adRecipientExpansion == null)
								{
									this.adRecipientExpansion = new ADRecipientExpansion(SMimeEventHandler.adRecipientProperties);
								}
								SMimeEventHandler.ADDistributionListExpansion addistributionListExpansion = new SMimeEventHandler.ADDistributionListExpansion(this.adRecipientExpansion, result.Data, this.expansionTimeoutTime);
								this.AddCerts(participant2, addistributionListExpansion.Certificats, addistributionListExpansion.Size, addistributionListExpansion.Certificats.Count);
								if (addistributionListExpansion.Size > addistributionListExpansion.Certificats.Count && this.allRecipients[SMimeEventHandler.RecipientGroup.AdRecipients].Contains(participant2))
								{
									this.getCertificateResponseWriter.RenderMissing(participant2.DisplayName, "ADDistList", Utilities.GetBase64StringFromADObjectId(result.Data[ADObjectSchema.Id] as ADObjectId), participant2.EmailAddress, addistributionListExpansion.Size, addistributionListExpansion.Certificats.Count);
								}
							}
							else if (!this.AddCert(participant2, Utilities.GetADRecipientCertificate(result.Data, checkRevocation)) && this.allRecipients[SMimeEventHandler.RecipientGroup.AdRecipients].Contains(participant2))
							{
								this.getCertificateResponseWriter.RenderMissing(ThemeFileId.DistributionListUser, participant2.DisplayName, "AD.RecipientType.User", Utilities.GetBase64StringFromADObjectId(result.Data[ADObjectSchema.Id] as ADObjectId));
							}
						}
					}
				}
			}
			if (hashSet.Count < recipients.Count)
			{
				foreach (Participant participant3 in recipients)
				{
					if (!hashSet.Contains(participant3))
					{
						this.getCertificateResponseWriter.RenderMissing(participant3.DisplayName);
					}
				}
			}
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x00111724 File Offset: 0x0010F924
		private IRecipientSession GetADRecipientSession()
		{
			if (this.adRecipientSession == null)
			{
				this.adRecipientSession = Utilities.CreateADRecipientSession(Culture.GetUserCulture().LCID, true, ConsistencyMode.FullyConsistent, true, base.UserContext);
			}
			return this.adRecipientSession;
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x00111754 File Offset: 0x0010F954
		[OwaEventParameter("isSend", typeof(bool))]
		[OwaEvent("ValidateCerts")]
		[OwaEventParameter("certs", typeof(string), true)]
		[OwaEventParameter("chains", typeof(string), true, true)]
		public void ValidateCerts()
		{
			string[] array = (string[])base.GetParameter("certs");
			string[] array2 = (string[])base.GetParameter("chains");
			bool flag = (bool)base.GetParameter("isSend");
			X509Store trustedStore = null;
			if (array2 != null)
			{
				trustedStore = this.CreateStore(array2);
			}
			bool flag2 = true;
			string text = null;
			this.Writer.Write("<p id=valRes>");
			foreach (string s in array)
			{
				ChainContext chainContext = null;
				try
				{
					X509Certificate2 x509Certificate = new X509Certificate2(Convert.FromBase64String(s));
					X509KeyUsageFlags expectedUsage;
					if (!flag || flag2)
					{
						expectedUsage = (X509KeyUsageFlags.NonRepudiation | X509KeyUsageFlags.DigitalSignature);
					}
					else
					{
						expectedUsage = X509KeyUsageFlags.KeyEncipherment;
					}
					ChainValidityStatus value = X509CertificateCollection.ValidateCertificate(x509Certificate, null, expectedUsage, flag ? OwaRegistryKeys.CheckCRLOnSend : (!OwaRegistryKeys.DisableCRLCheck), trustedStore, null, TimeSpan.FromMilliseconds((double)OwaRegistryKeys.CRLConnectionTimeout), TimeSpan.FromMilliseconds((double)OwaRegistryKeys.CRLRetrievalTimeout), ref chainContext, false, null);
					if (flag)
					{
						this.Writer.Write((uint)value);
						this.Writer.Write(" ");
					}
					else
					{
						string idFromCertificate = SMimeEventHandler.GetIdFromCertificate(x509Certificate);
						if (idFromCertificate == null)
						{
							value = ChainValidityStatus.SubjectMismatch;
						}
						this.Writer.Write("<p id=res>");
						this.Writer.Write((uint)value);
						this.Writer.Write("</p>");
						this.Writer.Write("<p id=eml>");
						this.Writer.Write(Utilities.HtmlEncode(idFromCertificate));
						this.Writer.Write("</p>");
						string displayName = X509PartialCertificate.GetDisplayName(x509Certificate);
						this.Writer.Write("<p id=cn>");
						this.Writer.Write(Utilities.HtmlEncode(string.IsNullOrEmpty(displayName) ? idFromCertificate : displayName));
						this.Writer.Write("</p>");
						this.Writer.Write("<p id=issuer>");
						this.Writer.Write(Utilities.HtmlEncode(SMimeEventHandler.GetIssuerDisplayNameFromCertificate(x509Certificate)));
						this.Writer.Write("</p>");
					}
					if (flag && flag2 && chainContext != null && OwaRegistryKeys.IncludeCertificateChainWithoutRootCertificate)
					{
						text = SMimeEventHandler.EncodeCertChain(chainContext, OwaRegistryKeys.IncludeCertificateChainAndRootCertificate, x509Certificate);
					}
				}
				finally
				{
					if (chainContext != null)
					{
						chainContext.Dispose();
					}
					flag2 = false;
				}
			}
			this.Writer.Write("</p>");
			if (text != null)
			{
				this.Writer.Write("<p id=chain>");
				this.Writer.Write(text);
				this.Writer.Write("</p>");
			}
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x001119E0 File Offset: 0x0010FBE0
		private X509Store CreateStore(string[] base64Certificates)
		{
			X509Store x509Store = CertificateStore.Open(StoreType.Memory, null, OpenFlags.ReadWrite);
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			foreach (string text in base64Certificates)
			{
				if (text != null)
				{
					X509Certificate2Collection x509Certificate2Collection2 = new X509Certificate2Collection();
					x509Certificate2Collection2.Import(Convert.FromBase64String(text));
					x509Certificate2Collection.AddRange(x509Certificate2Collection2);
				}
			}
			try
			{
				x509Store.AddRange(x509Certificate2Collection);
			}
			catch (SecurityException)
			{
				ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "Failed to add certificates to temporay memory store.");
			}
			return x509Store;
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x00111A68 File Offset: 0x0010FC68
		private bool FindCertificateByRoutingAddress(string routingAddress, out SMimeEventHandler.CertificatsInformation info)
		{
			foreach (KeyValuePair<Participant, SMimeEventHandler.CertificatsInformation> keyValuePair in this.certificateDictionary)
			{
				if (!(keyValuePair.Key.Origin is StoreParticipantOrigin) && string.Equals(keyValuePair.Key.RoutingType, "EX", StringComparison.OrdinalIgnoreCase) && string.Equals(keyValuePair.Key.EmailAddress, routingAddress, StringComparison.OrdinalIgnoreCase))
				{
					info = keyValuePair.Value;
					return true;
				}
			}
			info = null;
			return false;
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x00111B08 File Offset: 0x0010FD08
		internal static string EncodeCertChain(ChainContext chainContext, bool includeRootCert, X509Certificate2 signersCertificate)
		{
			uint count = (uint)chainContext.GetChains().Count;
			List<byte[]>[] array = new List<byte[]>[count];
			uint[] array2 = new uint[count];
			uint[] array3 = new uint[count];
			uint num = 16U;
			int num2 = 0;
			foreach (CertificateChain certificateChain in chainContext.GetChains())
			{
				array3[num2] = 16U;
				array2[num2] = 0U;
				array[num2] = new List<byte[]>(certificateChain.Elements.Count);
				foreach (ChainElement chainElement in certificateChain.Elements)
				{
					if (!signersCertificate.Equals(chainElement.Certificate) && (includeRootCert || (chainElement.TrustInformation & TrustInformation.IsSelfSigned) == TrustInformation.None))
					{
						array3[num2] += 12U;
						byte[] rawData = chainElement.Certificate.RawData;
						array[num2].Add(rawData);
						array3[num2] += (uint)rawData.Length;
						array2[num2] += 1U;
					}
				}
				num += array3[num2];
				num2++;
			}
			byte[] array4 = new byte[num];
			int num3 = 0;
			num3 += ExBitConverter.Write(num, array4, num3);
			num3 += ExBitConverter.Write((uint)chainContext.Status, array4, num3);
			num3 += ExBitConverter.Write((uint)chainContext.TrustInformation, array4, num3);
			num3 += ExBitConverter.Write(count, array4, num3);
			num2 = 0;
			foreach (CertificateChain certificateChain2 in chainContext.GetChains())
			{
				num3 += ExBitConverter.Write(array3[num2], array4, num3);
				num3 += ExBitConverter.Write((uint)certificateChain2.Status, array4, num3);
				num3 += ExBitConverter.Write((uint)certificateChain2.TrustInformation, array4, num3);
				num3 += ExBitConverter.Write(array2[num2], array4, num3);
				int num4 = 0;
				foreach (ChainElement chainElement2 in certificateChain2.Elements)
				{
					if (!signersCertificate.Equals(chainElement2.Certificate) && (includeRootCert || (chainElement2.TrustInformation & TrustInformation.IsSelfSigned) == TrustInformation.None))
					{
						num3 += ExBitConverter.Write((uint)(array[num2][num4].Length + 12), array4, num3);
						num3 += ExBitConverter.Write((uint)chainElement2.Status, array4, num3);
						num3 += ExBitConverter.Write((uint)chainElement2.TrustInformation, array4, num3);
						Array.Copy(array[num2][num4], 0, array4, num3, array[num2][num4].Length);
						num3 += array[num2][num4].Length;
						num4++;
					}
				}
				num2++;
			}
			return Convert.ToBase64String(array4);
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x00111E7C File Offset: 0x0011007C
		internal static string EncodeCertificates(ICollection<X509Certificate2> certificates, bool useKeyIdentifier)
		{
			List<byte[]> list = new List<byte[]>(certificates.Count);
			uint num = 12U;
			uint count = (uint)certificates.Count;
			foreach (X509Certificate2 certificate in certificates)
			{
				byte[] array = X509PartialCertificate.Encode(certificate, !useKeyIdentifier);
				list.Add(array);
				num += (uint)array.Length;
			}
			byte[] array2 = new byte[num];
			int num2 = 0;
			ExBitConverter.Write(num, array2, num2);
			num2 += 4;
			ExBitConverter.Write(count, array2, num2);
			num2 += 4;
			ExBitConverter.Write(count, array2, num2);
			num2 += 4;
			foreach (byte[] array3 in list)
			{
				Array.Copy(array3, 0, array2, num2, array3.Length);
				num2 += array3.Length;
			}
			return Convert.ToBase64String(array2);
		}

		// Token: 0x04002123 RID: 8483
		public const string EventNamespace = "SMime";

		// Token: 0x04002124 RID: 8484
		public const string MethodGetCerts = "GetCerts";

		// Token: 0x04002125 RID: 8485
		public const string MethodGetCertsInfo = "GetCertsInfo";

		// Token: 0x04002126 RID: 8486
		public const string MethodValidateCerts = "ValidateCerts";

		// Token: 0x04002127 RID: 8487
		public const string MethodUploadEmbeddedItem = "UploadEmbeddedItem";

		// Token: 0x04002128 RID: 8488
		public const string MimeBlob = "MimeBlob";

		// Token: 0x04002129 RID: 8489
		public const string IsVCard = "IsVCard";

		// Token: 0x0400212A RID: 8490
		public const string VCardCharset = "VCardCharset";

		// Token: 0x0400212B RID: 8491
		public const string AddressList = "addrs";

		// Token: 0x0400212C RID: 8492
		public const string Base64EncodedCerts = "certs";

		// Token: 0x0400212D RID: 8493
		public const string IsSend = "isSend";

		// Token: 0x0400212E RID: 8494
		public const string Base64EncodedChains = "chains";

		// Token: 0x0400212F RID: 8495
		private static PropertyDefinition[] adRecipientProperties = new PropertyDefinition[]
		{
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.LegacyExchangeDN,
			ADObjectSchema.Id,
			ADRecipientSchema.Certificate,
			ADRecipientSchema.SMimeCertificate,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.EmailAddresses,
			ADGroupSchema.HiddenGroupMembershipEnabled
		};

		// Token: 0x04002130 RID: 8496
		private static char[] comma = new char[]
		{
			','
		};

		// Token: 0x04002131 RID: 8497
		private static Oid commonNameOid = new Oid("2.5.4.3");

		// Token: 0x04002132 RID: 8498
		private IRecipientSession adRecipientSession;

		// Token: 0x04002133 RID: 8499
		private Dictionary<Participant, SMimeEventHandler.CertificatsInformation> certificateDictionary = new Dictionary<Participant, SMimeEventHandler.CertificatsInformation>();

		// Token: 0x04002134 RID: 8500
		private Dictionary<Participant, HashSet<Participant>> pdlExpansionResults = new Dictionary<Participant, HashSet<Participant>>();

		// Token: 0x04002135 RID: 8501
		private Dictionary<SMimeEventHandler.RecipientGroup, HashSet<Participant>> allRecipients;

		// Token: 0x04002136 RID: 8502
		private ADRecipientExpansion adRecipientExpansion;

		// Token: 0x04002137 RID: 8503
		private ExDateTime? expansionTimeoutTime;

		// Token: 0x04002138 RID: 8504
		private SMimeEventHandler.GetCertificateResponseWriter getCertificateResponseWriter;

		// Token: 0x020004DA RID: 1242
		private enum RecipientGroup
		{
			// Token: 0x0400213A RID: 8506
			PDLs,
			// Token: 0x0400213B RID: 8507
			Contacts,
			// Token: 0x0400213C RID: 8508
			AdRecipients,
			// Token: 0x0400213D RID: 8509
			Others
		}

		// Token: 0x020004DB RID: 1243
		private class CertificatsInformation
		{
			// Token: 0x06002F4C RID: 12108 RVA: 0x00112021 File Offset: 0x00110221
			public CertificatsInformation(List<X509Certificate2> certificates, int total, int valid)
			{
				this.Certificates = certificates;
				this.Total = total;
				this.Valid = valid;
			}

			// Token: 0x0400213E RID: 8510
			public readonly List<X509Certificate2> Certificates;

			// Token: 0x0400213F RID: 8511
			public readonly int Total;

			// Token: 0x04002140 RID: 8512
			public readonly int Valid;
		}

		// Token: 0x020004DC RID: 1244
		private class ADDistributionListExpansion
		{
			// Token: 0x06002F4D RID: 12109 RVA: 0x00112040 File Offset: 0x00110240
			public ADDistributionListExpansion(ADRecipientExpansion adRecipientExpansion, ADRawEntry recipient, ExDateTime? timeoutTime)
			{
				this.timeoutTime = timeoutTime;
				if (Utilities.IsADDistributionList((RecipientType)recipient[ADRecipientSchema.RecipientType]) && (bool)recipient[ADGroupSchema.HiddenGroupMembershipEnabled])
				{
					throw new SMimeEventHandler.HiddenMembershipException();
				}
				adRecipientExpansion.Expand(recipient, new ADRecipientExpansion.HandleRecipientDelegate(this.OnRecipient), new ADRecipientExpansion.HandleFailureDelegate(this.OnFailure));
			}

			// Token: 0x17000D44 RID: 3396
			// (get) Token: 0x06002F4E RID: 12110 RVA: 0x001120BE File Offset: 0x001102BE
			public int Size
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x17000D45 RID: 3397
			// (get) Token: 0x06002F4F RID: 12111 RVA: 0x001120C6 File Offset: 0x001102C6
			public List<X509Certificate2> Certificats
			{
				get
				{
					return this.certificats;
				}
			}

			// Token: 0x06002F50 RID: 12112 RVA: 0x001120D0 File Offset: 0x001102D0
			private ExpansionControl OnRecipient(ADRawEntry recipient, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentExpansionType)
			{
				this.CheckTimeout();
				bool flag = Utilities.IsADDistributionList((RecipientType)recipient[ADRecipientSchema.RecipientType]);
				if (flag && (bool)recipient[ADGroupSchema.HiddenGroupMembershipEnabled])
				{
					throw new SMimeEventHandler.HiddenMembershipException();
				}
				if (!flag)
				{
					string text = recipient[ADRecipientSchema.LegacyExchangeDN] as string;
					if (text != null && !this.foundMembersDNs.Contains(text))
					{
						this.size++;
						X509Certificate2 adrecipientCertificate = Utilities.GetADRecipientCertificate(recipient, false);
						if (adrecipientCertificate != null)
						{
							this.certificats.Add(adrecipientCertificate);
						}
						this.foundMembersDNs.Add(text);
					}
				}
				return ExpansionControl.Continue;
			}

			// Token: 0x06002F51 RID: 12113 RVA: 0x0011216B File Offset: 0x0011036B
			private ExpansionControl OnFailure(ExpansionFailure failure, ADRawEntry recipient, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentExpansionType)
			{
				this.CheckTimeout();
				return ExpansionControl.Continue;
			}

			// Token: 0x06002F52 RID: 12114 RVA: 0x00112174 File Offset: 0x00110374
			private void CheckTimeout()
			{
				if (this.timeoutTime == null)
				{
					return;
				}
				if (this.timeoutTime.Value < ExDateTime.UtcNow)
				{
					throw new SMimeEventHandler.ExpansionTimeoutException();
				}
			}

			// Token: 0x04002141 RID: 8513
			private List<X509Certificate2> certificats = new List<X509Certificate2>();

			// Token: 0x04002142 RID: 8514
			private int size;

			// Token: 0x04002143 RID: 8515
			private ExDateTime? timeoutTime;

			// Token: 0x04002144 RID: 8516
			private HashSet<string> foundMembersDNs = new HashSet<string>();
		}

		// Token: 0x020004DD RID: 1245
		private class SMimeEventHandlerException : Exception
		{
		}

		// Token: 0x020004DE RID: 1246
		private class HiddenMembershipException : SMimeEventHandler.SMimeEventHandlerException
		{
		}

		// Token: 0x020004DF RID: 1247
		private class ExpansionTimeoutException : SMimeEventHandler.SMimeEventHandlerException
		{
		}

		// Token: 0x020004E0 RID: 1248
		private abstract class GetCertificateResponseWriter
		{
			// Token: 0x06002F56 RID: 12118 RVA: 0x001121B9 File Offset: 0x001103B9
			public GetCertificateResponseWriter(UserContext userContext, TextWriter writer)
			{
				this.userContext = userContext;
				this.writer = writer;
			}

			// Token: 0x17000D46 RID: 3398
			// (get) Token: 0x06002F57 RID: 12119 RVA: 0x001121CF File Offset: 0x001103CF
			protected TextWriter Writer
			{
				get
				{
					return this.writer;
				}
			}

			// Token: 0x17000D47 RID: 3399
			// (get) Token: 0x06002F58 RID: 12120 RVA: 0x001121D7 File Offset: 0x001103D7
			protected UserContext UserContext
			{
				get
				{
					return this.userContext;
				}
			}

			// Token: 0x06002F59 RID: 12121
			public abstract void RenderMissing(ThemeFileId icon, string name, string type, string id);

			// Token: 0x06002F5A RID: 12122
			public abstract void RenderMissing(string name);

			// Token: 0x06002F5B RID: 12123
			public abstract void RenderMissing(string name, string type, string id, string legacyDN, int total, int valid);

			// Token: 0x06002F5C RID: 12124
			public abstract void RenderCertificates(Dictionary<Participant, SMimeEventHandler.CertificatsInformation> certificateDictionary);

			// Token: 0x06002F5D RID: 12125
			public abstract void RenderError(SMimeEventHandler.SMimeEventHandlerException e);

			// Token: 0x06002F5E RID: 12126 RVA: 0x001121E0 File Offset: 0x001103E0
			protected string GetDLMissingMessage(int total, int valid)
			{
				if (valid == 0)
				{
					return LocalizedStrings.GetNonEncoded(-969194198);
				}
				return string.Format(CultureInfo.CurrentCulture, LocalizedStrings.GetNonEncoded(-669360998), new object[]
				{
					total - valid,
					total
				});
			}

			// Token: 0x04002145 RID: 8517
			private readonly TextWriter writer;

			// Token: 0x04002146 RID: 8518
			private readonly UserContext userContext;
		}

		// Token: 0x020004E1 RID: 1249
		private sealed class GetCertificateInformationResponseWriter : SMimeEventHandler.GetCertificateResponseWriter
		{
			// Token: 0x06002F5F RID: 12127 RVA: 0x0011222B File Offset: 0x0011042B
			public GetCertificateInformationResponseWriter(UserContext userContext, TextWriter writer) : base(userContext, writer)
			{
			}

			// Token: 0x06002F60 RID: 12128 RVA: 0x00112235 File Offset: 0x00110435
			private void RenderMissingInformationStart()
			{
				this.missingInformationStarted = true;
				base.Writer.Write("<div id=mis>");
			}

			// Token: 0x06002F61 RID: 12129 RVA: 0x0011224E File Offset: 0x0011044E
			private void RenderMissingInformationEnd()
			{
				this.missingInformationStarted = false;
				base.Writer.Write("</div>");
			}

			// Token: 0x06002F62 RID: 12130 RVA: 0x00112267 File Offset: 0x00110467
			public override void RenderMissing(ThemeFileId icon, string name, string type, string id)
			{
				if (!this.missingInformationStarted)
				{
					this.RenderMissingInformationStart();
				}
				base.Writer.Write("<p _id=\"");
				Utilities.HtmlEncode(id, base.Writer);
				base.Writer.Write("\" _dl=0></p>");
			}

			// Token: 0x06002F63 RID: 12131 RVA: 0x001122A4 File Offset: 0x001104A4
			public override void RenderMissing(string name)
			{
			}

			// Token: 0x06002F64 RID: 12132 RVA: 0x001122A8 File Offset: 0x001104A8
			public override void RenderMissing(string name, string type, string id, string legacyDN, int total, int valid)
			{
				if (!this.missingInformationStarted)
				{
					this.RenderMissingInformationStart();
				}
				base.Writer.Write("<p");
				if (legacyDN != null)
				{
					base.Writer.Write(" _em=\"");
					Utilities.HtmlEncode(legacyDN, base.Writer);
				}
				else
				{
					base.Writer.Write(" _id=\"");
					Utilities.HtmlEncode(id, base.Writer);
				}
				base.Writer.Write("\" _dl=1 _tn=");
				base.Writer.Write(total);
				base.Writer.Write(" _vn=");
				base.Writer.Write(valid);
				base.Writer.Write(">");
				Utilities.HtmlEncode(base.GetDLMissingMessage(total, valid), base.Writer);
				base.Writer.Write("</p>");
			}

			// Token: 0x06002F65 RID: 12133 RVA: 0x00112381 File Offset: 0x00110581
			public override void RenderCertificates(Dictionary<Participant, SMimeEventHandler.CertificatsInformation> certificateDictionary)
			{
			}

			// Token: 0x06002F66 RID: 12134 RVA: 0x00112384 File Offset: 0x00110584
			public override void RenderError(SMimeEventHandler.SMimeEventHandlerException e)
			{
				if (this.missingInformationStarted)
				{
					this.RenderMissingInformationEnd();
				}
				base.Writer.Write("<p id=err _msg=\"");
				if (e is SMimeEventHandler.HiddenMembershipException)
				{
					base.Writer.Write(LocalizedStrings.GetHtmlEncoded(2141668304));
				}
				else
				{
					if (!(e is SMimeEventHandler.ExpansionTimeoutException))
					{
						throw new ArgumentOutOfRangeException("e", "Only support HiddenMembershipException and ExpansionTimeoutException");
					}
					base.Writer.Write(LocalizedStrings.GetHtmlEncoded(298218506));
				}
				base.Writer.Write("\"/>");
			}

			// Token: 0x04002147 RID: 8519
			private bool missingInformationStarted;
		}

		// Token: 0x020004E2 RID: 1250
		private sealed class GetCertificateListResponseWriter : SMimeEventHandler.GetCertificateResponseWriter
		{
			// Token: 0x06002F67 RID: 12135 RVA: 0x0011240D File Offset: 0x0011060D
			public GetCertificateListResponseWriter(UserContext userContext, TextWriter writer) : base(userContext, writer)
			{
			}

			// Token: 0x06002F68 RID: 12136 RVA: 0x00112418 File Offset: 0x00110618
			private void RenderMissingInformationStart()
			{
				this.missingInformationStarted = true;
				base.Writer.Write("<div id=mis><p>");
				base.Writer.Write(LocalizedStrings.GetHtmlEncoded(-1760660333));
				base.Writer.Write("</p><p>");
				base.Writer.Write(LocalizedStrings.GetHtmlEncoded(-1760660334));
				base.Writer.Write("</p><div id=dvMsCrts><table id=tblMsCrts cellpadding=0 cellspacing=0><tr class=hd><td class=\"ic ");
				base.Writer.Write(base.UserContext.IsRtl ? 'r' : 'l');
				base.Writer.Write("\">");
				base.UserContext.RenderThemeImage(base.Writer, ThemeFileId.Document);
				base.Writer.Write("</td><td>");
				base.Writer.Write(LocalizedStrings.GetHtmlEncoded(-1966747349));
				base.Writer.Write("</td></tr>");
			}

			// Token: 0x06002F69 RID: 12137 RVA: 0x00112500 File Offset: 0x00110700
			private void RenderMissingInformationEnd()
			{
				this.missingInformationStarted = false;
				base.Writer.Write("<tr id=trMsCrtsEL style=\"height:1;\"><td class=\"i ");
				base.Writer.Write(base.UserContext.IsRtl ? 'r' : 'l');
				base.Writer.Write("\"><img src=\"");
				base.UserContext.RenderThemeFileUrl(base.Writer, ThemeFileId.Clear1x1);
				base.Writer.Write("\"></td><td></td></tr></table></div></div>");
			}

			// Token: 0x06002F6A RID: 12138 RVA: 0x00112578 File Offset: 0x00110778
			private void RenderIcon(ThemeFileId themeFileId)
			{
				base.Writer.Write("<td class=\"i ");
				base.Writer.Write(base.UserContext.IsRtl ? 'r' : 'l');
				base.Writer.Write("\"><img src=\"");
				base.UserContext.RenderThemeFileUrl(base.Writer, themeFileId);
				base.Writer.Write("\"></td>");
			}

			// Token: 0x06002F6B RID: 12139 RVA: 0x001125E8 File Offset: 0x001107E8
			private void RenderName(string type, string id, string name, string missingInfo)
			{
				base.Writer.Write("<td nowrap>");
				if (type != null && id != null)
				{
					string handlerCode = string.Format("openItmRdFm(\"{0}\",\"{1}\");", Utilities.JavascriptEncode(type), Utilities.JavascriptEncode(id));
					base.Writer.Write("<a class=\"lnk");
					if (!string.IsNullOrEmpty(missingInfo))
					{
						base.Writer.Write(" bld");
					}
					base.Writer.Write("\" ");
					Utilities.RenderScriptHandler(base.Writer, "onclick", handlerCode);
					base.Writer.Write(" title=\"");
					Utilities.HtmlEncode(name, base.Writer);
					base.Writer.Write("\">");
					Utilities.HtmlEncode(name, base.Writer);
					base.Writer.Write("</a>");
					if (!string.IsNullOrEmpty(missingInfo))
					{
						base.Writer.Write(" <span class=spnFnd>");
						base.Writer.Write(base.UserContext.DirectionMark);
						Utilities.HtmlEncode(missingInfo, base.Writer);
						base.Writer.Write(base.UserContext.DirectionMark);
						base.Writer.Write("</span>");
					}
				}
				else
				{
					Utilities.HtmlEncode(name, base.Writer);
				}
				base.Writer.Write("</td>");
			}

			// Token: 0x06002F6C RID: 12140 RVA: 0x0011273C File Offset: 0x0011093C
			public override void RenderMissing(ThemeFileId icon, string name, string type, string id)
			{
				if (!this.missingInformationStarted)
				{
					this.RenderMissingInformationStart();
				}
				base.Writer.Write("<tr>");
				this.RenderIcon(icon);
				this.RenderName(type, id, name, null);
				base.Writer.Write("</tr>");
			}

			// Token: 0x06002F6D RID: 12141 RVA: 0x00112789 File Offset: 0x00110989
			public override void RenderMissing(string name)
			{
				if (string.IsNullOrEmpty(name))
				{
					return;
				}
				this.RenderMissing(ThemeFileId.DistributionListUser, name, null, null);
			}

			// Token: 0x06002F6E RID: 12142 RVA: 0x001127A4 File Offset: 0x001109A4
			public override void RenderMissing(string name, string type, string id, string legacyDN, int total, int valid)
			{
				if (!this.missingInformationStarted)
				{
					this.RenderMissingInformationStart();
				}
				base.Writer.Write("<tr>");
				this.RenderIcon(ThemeFileId.DistributionListOther);
				this.RenderName(type, id, name, base.GetDLMissingMessage(total, valid));
				base.Writer.Write("</tr>");
			}

			// Token: 0x06002F6F RID: 12143 RVA: 0x00112800 File Offset: 0x00110A00
			public override void RenderError(SMimeEventHandler.SMimeEventHandlerException e)
			{
				if (this.missingInformationStarted)
				{
					this.RenderMissingInformationEnd();
				}
				base.Writer.Write("<p id=err _msg=\"");
				if (e is SMimeEventHandler.HiddenMembershipException)
				{
					base.Writer.Write(LocalizedStrings.GetHtmlEncoded(1161561076));
				}
				else
				{
					if (!(e is SMimeEventHandler.ExpansionTimeoutException))
					{
						throw new ArgumentOutOfRangeException("e", "Only support HiddenMembershipException and ExpansionTimeoutException");
					}
					base.Writer.Write(LocalizedStrings.GetHtmlEncoded(1073923836));
				}
				base.Writer.Write("\"/>");
			}

			// Token: 0x06002F70 RID: 12144 RVA: 0x0011288C File Offset: 0x00110A8C
			public override void RenderCertificates(Dictionary<Participant, SMimeEventHandler.CertificatsInformation> certificateDictionary)
			{
				if (this.missingInformationStarted)
				{
					this.RenderMissingInformationEnd();
				}
				if (certificateDictionary.Count == 0)
				{
					return;
				}
				bool flag = false;
				foreach (KeyValuePair<Participant, SMimeEventHandler.CertificatsInformation> keyValuePair in certificateDictionary)
				{
					Participant key = keyValuePair.Key;
					SMimeEventHandler.CertificatsInformation value = keyValuePair.Value;
					if (value.Certificates.Count != 0)
					{
						if (!flag)
						{
							flag = true;
							base.Writer.WriteLine("<div id=\"certs\">");
						}
						string s;
						try
						{
							s = SMimeEventHandler.EncodeCertificates(value.Certificates, OwaRegistryKeys.UseKeyIdentifier);
						}
						catch (CryptographicException)
						{
							continue;
						}
						StoreParticipantOrigin storeParticipantOrigin = key.Origin as StoreParticipantOrigin;
						if (storeParticipantOrigin != null)
						{
							base.Writer.Write("<p _id=\"");
							Utilities.HtmlEncode((key.Origin as StoreParticipantOrigin).OriginItemId.ToBase64String(), base.Writer);
							base.Writer.Write("\"");
						}
						else
						{
							base.Writer.Write("<p _em=\"");
							Utilities.HtmlEncode(key.EmailAddress, base.Writer);
							base.Writer.Write("\"");
						}
						base.Writer.Write(" _tn=");
						base.Writer.Write(value.Total);
						base.Writer.Write(" _vn=");
						base.Writer.Write(value.Valid);
						base.Writer.Write(">");
						Utilities.HtmlEncode(s, base.Writer);
						base.Writer.WriteLine("</p>");
					}
				}
				if (flag)
				{
					base.Writer.WriteLine("</div>");
				}
			}

			// Token: 0x04002148 RID: 8520
			private bool missingInformationStarted;
		}
	}
}
