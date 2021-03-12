using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MessageSecurity.EdgeSync
{
	// Token: 0x0200001A RID: 26
	internal class XmlSerializationReaderEdgeSyncCredential : XmlSerializationReader
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00005074 File Offset: 0x00003274
		public object Read3_EdgeSyncCredential()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_EdgeSyncCredential || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = this.Read2_EdgeSyncCredential(true, true);
			}
			else
			{
				base.UnknownNode(null, ":EdgeSyncCredential");
			}
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000050E4 File Offset: 0x000032E4
		private EdgeSyncCredential Read2_EdgeSyncCredential(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id1_EdgeSyncCredential || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			EdgeSyncCredential edgeSyncCredential = new EdgeSyncCredential();
			bool[] array = new bool[7];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(edgeSyncCredential);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return edgeSyncCredential;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id3_EdgeServerFQDN && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSyncCredential.EdgeServerFQDN = base.Reader.ReadElementString();
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id4_ESRAUsername && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSyncCredential.ESRAUsername = base.Reader.ReadElementString();
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id5_EncryptedESRAPassword && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSyncCredential.EncryptedESRAPassword = base.ToByteArrayBase64(false);
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id6_EdgeEncryptedESRAPassword && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSyncCredential.EdgeEncryptedESRAPassword = base.ToByteArrayBase64(false);
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id7_EffectiveDate && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSyncCredential.EffectiveDate = XmlConvert.ToInt64(base.Reader.ReadElementString());
						array[4] = true;
					}
					else if (!array[5] && base.Reader.LocalName == this.id8_Duration && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSyncCredential.Duration = XmlConvert.ToInt64(base.Reader.ReadElementString());
						array[5] = true;
					}
					else if (!array[6] && base.Reader.LocalName == this.id9_IsBootStrapAccount && base.Reader.NamespaceURI == this.id2_Item)
					{
						edgeSyncCredential.IsBootStrapAccount = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[6] = true;
					}
					else
					{
						base.UnknownNode(edgeSyncCredential, ":EdgeServerFQDN, :ESRAUsername, :EncryptedESRAPassword, :EdgeEncryptedESRAPassword, :EffectiveDate, :Duration, :IsBootStrapAccount");
					}
				}
				else
				{
					base.UnknownNode(edgeSyncCredential, ":EdgeServerFQDN, :ESRAUsername, :EncryptedESRAPassword, :EdgeEncryptedESRAPassword, :EffectiveDate, :Duration, :IsBootStrapAccount");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return edgeSyncCredential;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000540D File Offset: 0x0000360D
		protected override void InitCallbacks()
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005410 File Offset: 0x00003610
		protected override void InitIDs()
		{
			this.id9_IsBootStrapAccount = base.Reader.NameTable.Add("IsBootStrapAccount");
			this.id3_EdgeServerFQDN = base.Reader.NameTable.Add("EdgeServerFQDN");
			this.id8_Duration = base.Reader.NameTable.Add("Duration");
			this.id7_EffectiveDate = base.Reader.NameTable.Add("EffectiveDate");
			this.id2_Item = base.Reader.NameTable.Add("");
			this.id6_EdgeEncryptedESRAPassword = base.Reader.NameTable.Add("EdgeEncryptedESRAPassword");
			this.id5_EncryptedESRAPassword = base.Reader.NameTable.Add("EncryptedESRAPassword");
			this.id4_ESRAUsername = base.Reader.NameTable.Add("ESRAUsername");
			this.id1_EdgeSyncCredential = base.Reader.NameTable.Add("EdgeSyncCredential");
		}

		// Token: 0x04000076 RID: 118
		private string id9_IsBootStrapAccount;

		// Token: 0x04000077 RID: 119
		private string id3_EdgeServerFQDN;

		// Token: 0x04000078 RID: 120
		private string id8_Duration;

		// Token: 0x04000079 RID: 121
		private string id7_EffectiveDate;

		// Token: 0x0400007A RID: 122
		private string id2_Item;

		// Token: 0x0400007B RID: 123
		private string id6_EdgeEncryptedESRAPassword;

		// Token: 0x0400007C RID: 124
		private string id5_EncryptedESRAPassword;

		// Token: 0x0400007D RID: 125
		private string id4_ESRAUsername;

		// Token: 0x0400007E RID: 126
		private string id1_EdgeSyncCredential;
	}
}
