using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x020008AB RID: 2219
	public class XmlSerializationReaderRecipientSyncState : XmlSerializationReader
	{
		// Token: 0x06002F97 RID: 12183 RVA: 0x0006C254 File Offset: 0x0006A454
		public object Read3_RecipientSyncState()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_RecipientSyncState || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = this.Read2_RecipientSyncState(true, true);
			}
			else
			{
				base.UnknownNode(null, ":RecipientSyncState");
			}
			return result;
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x0006C2C4 File Offset: 0x0006A4C4
		private RecipientSyncState Read2_RecipientSyncState(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id1_RecipientSyncState || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			RecipientSyncState recipientSyncState = new RecipientSyncState();
			bool[] array = new bool[5];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(recipientSyncState);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return recipientSyncState;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id3_ProxyAddresses && base.Reader.NamespaceURI == this.id2_Item)
					{
						recipientSyncState.ProxyAddresses = base.Reader.ReadElementString();
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id4_SignupAddresses && base.Reader.NamespaceURI == this.id2_Item)
					{
						recipientSyncState.SignupAddresses = base.Reader.ReadElementString();
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id5_PartnerId && base.Reader.NamespaceURI == this.id2_Item)
					{
						recipientSyncState.PartnerId = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id6_UMProxyAddresses && base.Reader.NamespaceURI == this.id2_Item)
					{
						recipientSyncState.UMProxyAddresses = base.Reader.ReadElementString();
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id7_ArchiveAddress && base.Reader.NamespaceURI == this.id2_Item)
					{
						recipientSyncState.ArchiveAddress = base.Reader.ReadElementString();
						array[4] = true;
					}
					else
					{
						base.UnknownNode(recipientSyncState, ":ProxyAddresses, :SignupAddresses, :PartnerId, :UMProxyAddresses, :ArchiveAddress");
					}
				}
				else
				{
					base.UnknownNode(recipientSyncState, ":ProxyAddresses, :SignupAddresses, :PartnerId, :UMProxyAddresses, :ArchiveAddress");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return recipientSyncState;
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x0006C561 File Offset: 0x0006A761
		protected override void InitCallbacks()
		{
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x0006C564 File Offset: 0x0006A764
		protected override void InitIDs()
		{
			this.id6_UMProxyAddresses = base.Reader.NameTable.Add("UMProxyAddresses");
			this.id3_ProxyAddresses = base.Reader.NameTable.Add("ProxyAddresses");
			this.id4_SignupAddresses = base.Reader.NameTable.Add("SignupAddresses");
			this.id2_Item = base.Reader.NameTable.Add("");
			this.id7_ArchiveAddress = base.Reader.NameTable.Add("ArchiveAddress");
			this.id5_PartnerId = base.Reader.NameTable.Add("PartnerId");
			this.id1_RecipientSyncState = base.Reader.NameTable.Add("RecipientSyncState");
		}

		// Token: 0x04002937 RID: 10551
		private string id6_UMProxyAddresses;

		// Token: 0x04002938 RID: 10552
		private string id3_ProxyAddresses;

		// Token: 0x04002939 RID: 10553
		private string id4_SignupAddresses;

		// Token: 0x0400293A RID: 10554
		private string id2_Item;

		// Token: 0x0400293B RID: 10555
		private string id7_ArchiveAddress;

		// Token: 0x0400293C RID: 10556
		private string id5_PartnerId;

		// Token: 0x0400293D RID: 10557
		private string id1_RecipientSyncState;
	}
}
