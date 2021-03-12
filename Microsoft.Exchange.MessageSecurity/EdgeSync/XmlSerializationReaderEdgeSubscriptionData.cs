using System;
using System.Reflection;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MessageSecurity.EdgeSync
{
	// Token: 0x02000015 RID: 21
	internal class XmlSerializationReaderEdgeSubscriptionData : XmlSerializationReader
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00004668 File Offset: 0x00002868
		public object Read3_EdgeSubscriptionData()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_EdgeSubscriptionData || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = this.Read2_EdgeSubscriptionData(true);
			}
			else
			{
				base.UnknownNode(null, ":EdgeSubscriptionData");
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000046DC File Offset: 0x000028DC
		private EdgeSubscriptionData Read2_EdgeSubscriptionData(bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id1_EdgeSubscriptionData || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			EdgeSubscriptionData edgeSubscriptionData;
			try
			{
				edgeSubscriptionData = (EdgeSubscriptionData)Activator.CreateInstance(typeof(EdgeSubscriptionData), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[0], null);
			}
			catch (MissingMethodException)
			{
				throw base.CreateInaccessibleConstructorException("global::Microsoft.Exchange.MessageSecurity.EdgeSync.EdgeSubscriptionData");
			}
			catch (SecurityException)
			{
				throw base.CreateCtorHasSecurityException("global::Microsoft.Exchange.MessageSecurity.EdgeSync.EdgeSubscriptionData");
			}
			bool[] array = new bool[13];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(edgeSubscriptionData);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return edgeSubscriptionData;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id3_EdgeServerName && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSubscriptionData.EdgeServerName = base.Reader.ReadElementString();
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id4_EdgeServerFQDN && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSubscriptionData.EdgeServerFQDN = base.Reader.ReadElementString();
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id5_EdgeCertificateBlob && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSubscriptionData.EdgeCertificateBlob = base.ToByteArrayBase64(false);
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id6_PfxKPKCertificateBlob && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSubscriptionData.PfxKPKCertificateBlob = base.ToByteArrayBase64(false);
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id7_ESRAUsername && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSubscriptionData.ESRAUsername = base.Reader.ReadElementString();
						array[4] = true;
					}
					else if (!array[5] && base.Reader.LocalName == this.id8_ESRAPassword && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSubscriptionData.ESRAPassword = base.Reader.ReadElementString();
						array[5] = true;
					}
					else if (!array[6] && base.Reader.LocalName == this.id9_EffectiveDate && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSubscriptionData.EffectiveDate = XmlConvert.ToInt64(base.Reader.ReadElementString());
						array[6] = true;
					}
					else if (!array[7] && base.Reader.LocalName == this.id10_Duration && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSubscriptionData.Duration = XmlConvert.ToInt64(base.Reader.ReadElementString());
						array[7] = true;
					}
					else if (!array[8] && base.Reader.LocalName == this.id11_AdamSslPort && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSubscriptionData.AdamSslPort = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[8] = true;
					}
					else if (!array[9] && base.Reader.LocalName == this.id12_ServerType && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSubscriptionData.ServerType = base.Reader.ReadElementString();
						array[9] = true;
					}
					else if (!array[10] && base.Reader.LocalName == this.id13_ProductID && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSubscriptionData.ProductID = base.Reader.ReadElementString();
						array[10] = true;
					}
					else if (!array[11] && base.Reader.LocalName == this.id14_VersionNumber && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSubscriptionData.VersionNumber = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[11] = true;
					}
					else if (!array[12] && base.Reader.LocalName == this.id15_SerialNumber && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSubscriptionData.SerialNumber = base.Reader.ReadElementString();
						array[12] = true;
					}
					else
					{
						base.UnknownNode(edgeSubscriptionData, ":EdgeServerName, :EdgeServerFQDN, :EdgeCertificateBlob, :PfxKPKCertificateBlob, :ESRAUsername, :ESRAPassword, :EffectiveDate, :Duration, :AdamSslPort, :ServerType, :ProductID, :VersionNumber, :SerialNumber");
					}
				}
				else
				{
					base.UnknownNode(edgeSubscriptionData, ":EdgeServerName, :EdgeServerFQDN, :EdgeCertificateBlob, :PfxKPKCertificateBlob, :ESRAUsername, :ESRAPassword, :EffectiveDate, :Duration, :AdamSslPort, :ServerType, :ProductID, :VersionNumber, :SerialNumber");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return edgeSubscriptionData;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004C10 File Offset: 0x00002E10
		protected override void InitCallbacks()
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004C14 File Offset: 0x00002E14
		protected override void InitIDs()
		{
			this.id13_ProductID = base.Reader.NameTable.Add("ProductID");
			this.id15_SerialNumber = base.Reader.NameTable.Add("SerialNumber");
			this.id8_ESRAPassword = base.Reader.NameTable.Add("ESRAPassword");
			this.id4_EdgeServerFQDN = base.Reader.NameTable.Add("EdgeServerFQDN");
			this.id12_ServerType = base.Reader.NameTable.Add("ServerType");
			this.id3_EdgeServerName = base.Reader.NameTable.Add("EdgeServerName");
			this.id9_EffectiveDate = base.Reader.NameTable.Add("EffectiveDate");
			this.id2_Item = base.Reader.NameTable.Add("");
			this.id5_EdgeCertificateBlob = base.Reader.NameTable.Add("EdgeCertificateBlob");
			this.id14_VersionNumber = base.Reader.NameTable.Add("VersionNumber");
			this.id10_Duration = base.Reader.NameTable.Add("Duration");
			this.id1_EdgeSubscriptionData = base.Reader.NameTable.Add("EdgeSubscriptionData");
			this.id7_ESRAUsername = base.Reader.NameTable.Add("ESRAUsername");
			this.id11_AdamSslPort = base.Reader.NameTable.Add("AdamSslPort");
			this.id6_PfxKPKCertificateBlob = base.Reader.NameTable.Add("PfxKPKCertificateBlob");
		}

		// Token: 0x04000064 RID: 100
		private string id13_ProductID;

		// Token: 0x04000065 RID: 101
		private string id15_SerialNumber;

		// Token: 0x04000066 RID: 102
		private string id8_ESRAPassword;

		// Token: 0x04000067 RID: 103
		private string id4_EdgeServerFQDN;

		// Token: 0x04000068 RID: 104
		private string id12_ServerType;

		// Token: 0x04000069 RID: 105
		private string id3_EdgeServerName;

		// Token: 0x0400006A RID: 106
		private string id9_EffectiveDate;

		// Token: 0x0400006B RID: 107
		private string id2_Item;

		// Token: 0x0400006C RID: 108
		private string id5_EdgeCertificateBlob;

		// Token: 0x0400006D RID: 109
		private string id14_VersionNumber;

		// Token: 0x0400006E RID: 110
		private string id10_Duration;

		// Token: 0x0400006F RID: 111
		private string id1_EdgeSubscriptionData;

		// Token: 0x04000070 RID: 112
		private string id7_ESRAUsername;

		// Token: 0x04000071 RID: 113
		private string id11_AdamSslPort;

		// Token: 0x04000072 RID: 114
		private string id6_PfxKPKCertificateBlob;
	}
}
