using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000117 RID: 279
	internal class ResolvedRecipient : IComparable
	{
		// Token: 0x06000ECB RID: 3787 RVA: 0x0005444E File Offset: 0x0005264E
		internal ResolvedRecipient(RecipientAddress address)
		{
			this.ResolvedTo = address;
			this.CertificateRetrieval = ResolveRecipientsCommand.CertificateRetrievalType.None;
			this.Certificates = new X509Certificate2Collection();
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0005446F File Offset: 0x0005266F
		// (set) Token: 0x06000ECD RID: 3789 RVA: 0x00054477 File Offset: 0x00052677
		internal ResolveRecipientsCommand.CertificateRetrievalType CertificateRetrieval { get; set; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00054480 File Offset: 0x00052680
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x00054488 File Offset: 0x00052688
		internal int CertificateCount { get; set; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00054491 File Offset: 0x00052691
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x00054499 File Offset: 0x00052699
		internal int CertificateRecipientCount { get; set; }

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x000544A2 File Offset: 0x000526A2
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x000544AA File Offset: 0x000526AA
		internal X509Certificate2Collection Certificates { get; set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x000544B3 File Offset: 0x000526B3
		// (set) Token: 0x06000ED5 RID: 3797 RVA: 0x000544BB File Offset: 0x000526BB
		internal bool GlobalCertLimitWasHit { get; set; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x000544C4 File Offset: 0x000526C4
		// (set) Token: 0x06000ED7 RID: 3799 RVA: 0x000544CC File Offset: 0x000526CC
		internal RecipientAddress ResolvedTo { get; set; }

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x000544D5 File Offset: 0x000526D5
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x000544DD File Offset: 0x000526DD
		internal byte[] Picture { get; set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x000544E6 File Offset: 0x000526E6
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x000544EE File Offset: 0x000526EE
		internal StatusCode AvailabilityStatus { get; set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x000544F7 File Offset: 0x000526F7
		// (set) Token: 0x06000EDD RID: 3805 RVA: 0x000544FF File Offset: 0x000526FF
		internal string MergedFreeBusy { get; set; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00054508 File Offset: 0x00052708
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x00054510 File Offset: 0x00052710
		internal PictureOptions PictureOptions { get; set; }

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0005451C File Offset: 0x0005271C
		public int CompareTo(object value)
		{
			ResolvedRecipient resolvedRecipient = value as ResolvedRecipient;
			if (resolvedRecipient != null)
			{
				return this.ResolvedTo.CompareTo(resolvedRecipient.ResolvedTo);
			}
			throw new ArgumentException("object is not an ResolvedRecipient");
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00054550 File Offset: 0x00052750
		internal void BuildXmlResponse(XmlDocument xmlResponse, XmlNode parentNode, bool pictureLimitReached, out bool pictureWasAdded)
		{
			pictureWasAdded = false;
			XmlNode xmlNode = xmlResponse.CreateElement("Recipient", "ResolveRecipients:");
			parentNode.AppendChild(xmlNode);
			XmlNode xmlNode2 = xmlResponse.CreateElement("Type", "ResolveRecipients:");
			xmlNode2.InnerText = ((this.ResolvedTo.AddressOrigin == AddressOrigin.Directory) ? "1" : "2");
			xmlNode.AppendChild(xmlNode2);
			XmlNode xmlNode3 = xmlResponse.CreateElement("DisplayName", "ResolveRecipients:");
			xmlNode3.InnerText = this.ResolvedTo.DisplayName;
			xmlNode.AppendChild(xmlNode3);
			XmlNode xmlNode4 = xmlResponse.CreateElement("EmailAddress", "ResolveRecipients:");
			xmlNode4.InnerText = this.ResolvedTo.SmtpAddress;
			xmlNode.AppendChild(xmlNode4);
			if (this.PictureOptions != null)
			{
				StatusCode statusCode = StatusCode.Success;
				byte[] array = null;
				if (Command.CurrentCommand.User.Features.IsEnabled(EasFeature.HDPhotos) && Command.CurrentCommand.Request.Version >= 160)
				{
					ResolveRecipientsCommand resolveRecipientsCommand = Command.CurrentCommand as ResolveRecipientsCommand;
					if (resolveRecipientsCommand != null && resolveRecipientsCommand.PhotoRetriever != null)
					{
						array = resolveRecipientsCommand.PhotoRetriever.EndGetThumbnailPhotoFromMailbox(this.ResolvedTo.SmtpAddress, GlobalSettings.MaxRequestExecutionTime - ExDateTime.Now.Subtract(Command.CurrentCommand.Context.RequestTime), this.PictureOptions.PhotoSize);
					}
					else
					{
						AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.RequestsTracer, this, "Error getting PhotoRetriever instance from Command. Command:{0}", (Command.CurrentCommand == null) ? "<null>" : Command.CurrentCommand.Request.CommandType.ToString());
					}
				}
				if (statusCode != StatusCode.Success || array == null)
				{
					array = this.Picture;
				}
				XmlNode newChild = this.PictureOptions.CreatePictureNode(xmlNode.OwnerDocument, "ResolveRecipients:", array, pictureLimitReached, out pictureWasAdded);
				xmlNode.AppendChild(newChild);
			}
			if (this.CertificateRetrieval != ResolveRecipientsCommand.CertificateRetrievalType.None)
			{
				XmlNode xmlNode5 = xmlResponse.CreateElement("Certificates", "ResolveRecipients:");
				xmlNode.AppendChild(xmlNode5);
				XmlNode xmlNode6 = xmlResponse.CreateElement("Status", "ResolveRecipients:");
				xmlNode5.AppendChild(xmlNode6);
				if (!this.GlobalCertLimitWasHit && (this.Certificates == null || this.Certificates.Count == 0))
				{
					xmlNode6.InnerText = 7.ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					if (this.GlobalCertLimitWasHit)
					{
						xmlNode6.InnerText = 8.ToString(CultureInfo.InvariantCulture);
					}
					else
					{
						xmlNode6.InnerText = 1.ToString(CultureInfo.InvariantCulture);
					}
					XmlNode xmlNode7 = xmlResponse.CreateElement("CertificateCount", "ResolveRecipients:");
					xmlNode7.InnerText = this.CertificateCount.ToString(CultureInfo.InvariantCulture);
					xmlNode5.AppendChild(xmlNode7);
					XmlNode xmlNode8 = xmlResponse.CreateElement("RecipientCount", "ResolveRecipients:");
					xmlNode8.InnerText = this.CertificateRecipientCount.ToString(CultureInfo.InvariantCulture);
					xmlNode5.AppendChild(xmlNode8);
					if (this.CertificateRetrieval == ResolveRecipientsCommand.CertificateRetrievalType.Full)
					{
						foreach (X509Certificate2 x509Certificate in this.Certificates)
						{
							XmlNode xmlNode9 = xmlResponse.CreateElement("Certificate", "ResolveRecipients:");
							xmlNode9.InnerText = Convert.ToBase64String(x509Certificate.RawData);
							xmlNode5.AppendChild(xmlNode9);
						}
					}
					else
					{
						foreach (X509Certificate2 certificate in this.Certificates)
						{
							XmlNode xmlNode10 = xmlResponse.CreateElement("MiniCertificate", "ResolveRecipients:");
							byte[] inArray = X509PartialCertificate.Encode(certificate, true);
							xmlNode10.InnerText = Convert.ToBase64String(inArray);
							xmlNode5.AppendChild(xmlNode10);
						}
					}
				}
			}
			if (this.AvailabilityStatus != StatusCode.None)
			{
				XmlNode xmlNode11 = xmlResponse.CreateElement("Availability", "ResolveRecipients:");
				xmlNode.AppendChild(xmlNode11);
				XmlNode xmlNode12 = xmlResponse.CreateElement("Status", "ResolveRecipients:");
				xmlNode12.InnerText = ((int)this.AvailabilityStatus).ToString(CultureInfo.InvariantCulture);
				xmlNode11.AppendChild(xmlNode12);
				if (!string.IsNullOrEmpty(this.MergedFreeBusy))
				{
					XmlNode xmlNode13 = xmlResponse.CreateElement("MergedFreeBusy", "ResolveRecipients:");
					xmlNode13.InnerText = this.MergedFreeBusy;
					xmlNode11.AppendChild(xmlNode13);
					return;
				}
				if (this.AvailabilityStatus == StatusCode.Success)
				{
					throw new InvalidOperationException("Empty free busy string received!");
				}
			}
		}
	}
}
