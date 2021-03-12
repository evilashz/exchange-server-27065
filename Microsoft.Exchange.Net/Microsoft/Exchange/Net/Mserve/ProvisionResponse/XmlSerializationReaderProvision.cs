using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.ProvisionResponse
{
	// Token: 0x020008A3 RID: 2211
	internal class XmlSerializationReaderProvision : XmlSerializationReader
	{
		// Token: 0x06002F5C RID: 12124 RVA: 0x0006AD2C File Offset: 0x00068F2C
		public object Read6_Provision()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_Provision || base.Reader.NamespaceURI != this.id2_DeltaSyncV2)
				{
					throw base.CreateUnknownNodeException();
				}
				result = this.Read5_Provision(false, true);
			}
			else
			{
				base.UnknownNode(null, "DeltaSyncV2::Provision");
			}
			return result;
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x0006AD9C File Offset: 0x00068F9C
		private Provision Read5_Provision(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_DeltaSyncV2))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			Provision provision = new Provision();
			bool[] array = new bool[3];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(provision);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return provision;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id4_Status && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
					{
						provision.Status = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id5_Fault && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
					{
						provision.Fault = this.Read2_Fault(false, true);
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id6_Responses && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
					{
						provision.Responses = this.Read4_ProvisionResponses(false, true);
						array[2] = true;
					}
					else
					{
						base.UnknownNode(provision, "DeltaSyncV2::Status, DeltaSyncV2::Fault, DeltaSyncV2::Responses");
					}
				}
				else
				{
					base.UnknownNode(provision, "DeltaSyncV2::Status, DeltaSyncV2::Fault, DeltaSyncV2::Responses");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return provision;
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x0006AFAC File Offset: 0x000691AC
		private ProvisionResponses Read4_ProvisionResponses(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_DeltaSyncV2))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			ProvisionResponses provisionResponses = new ProvisionResponses();
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(provisionResponses);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return provisionResponses;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (base.Reader.LocalName == this.id7_Add && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
					{
						if (!base.ReadNull())
						{
							AccountType[] array = null;
							int num2 = 0;
							if (base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num3 = 0;
								int readerCount2 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id8_Account && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
										{
											array = (AccountType[])base.EnsureArrayIndex(array, num2, typeof(AccountType));
											array[num2++] = this.Read3_AccountType(false, true);
										}
										else
										{
											base.UnknownNode(null, "DeltaSyncV2::Account");
										}
									}
									else
									{
										base.UnknownNode(null, "DeltaSyncV2::Account");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num3, ref readerCount2);
								}
								base.ReadEndElement();
							}
							provisionResponses.Add = (AccountType[])base.ShrinkArray(array, num2, typeof(AccountType), false);
						}
					}
					else if (base.Reader.LocalName == this.id9_Delete && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
					{
						if (!base.ReadNull())
						{
							AccountType[] array2 = null;
							int num4 = 0;
							if (base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num5 = 0;
								int readerCount3 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id8_Account && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
										{
											array2 = (AccountType[])base.EnsureArrayIndex(array2, num4, typeof(AccountType));
											array2[num4++] = this.Read3_AccountType(false, true);
										}
										else
										{
											base.UnknownNode(null, "DeltaSyncV2::Account");
										}
									}
									else
									{
										base.UnknownNode(null, "DeltaSyncV2::Account");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num5, ref readerCount3);
								}
								base.ReadEndElement();
							}
							provisionResponses.Delete = (AccountType[])base.ShrinkArray(array2, num4, typeof(AccountType), false);
						}
					}
					else if (base.Reader.LocalName == this.id10_Read && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
					{
						if (!base.ReadNull())
						{
							AccountType[] array3 = null;
							int num6 = 0;
							if (base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num7 = 0;
								int readerCount4 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id8_Account && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
										{
											array3 = (AccountType[])base.EnsureArrayIndex(array3, num6, typeof(AccountType));
											array3[num6++] = this.Read3_AccountType(false, true);
										}
										else
										{
											base.UnknownNode(null, "DeltaSyncV2::Account");
										}
									}
									else
									{
										base.UnknownNode(null, "DeltaSyncV2::Account");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num7, ref readerCount4);
								}
								base.ReadEndElement();
							}
							provisionResponses.Read = (AccountType[])base.ShrinkArray(array3, num6, typeof(AccountType), false);
						}
					}
					else
					{
						base.UnknownNode(provisionResponses, "DeltaSyncV2::Add, DeltaSyncV2::Delete, DeltaSyncV2::Read");
					}
				}
				else
				{
					base.UnknownNode(provisionResponses, "DeltaSyncV2::Add, DeltaSyncV2::Delete, DeltaSyncV2::Read");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return provisionResponses;
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x0006B500 File Offset: 0x00069700
		private AccountType Read3_AccountType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id11_AccountType || xmlQualifiedName.Namespace != this.id2_DeltaSyncV2))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			AccountType accountType = new AccountType();
			bool[] array = new bool[4];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(accountType);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return accountType;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id12_Name && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
					{
						accountType.Name = base.Reader.ReadElementString();
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id4_Status && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
					{
						accountType.Status = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id5_Fault && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
					{
						accountType.Fault = this.Read2_Fault(false, true);
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id13_PartnerID && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
					{
						accountType.PartnerID = base.Reader.ReadElementString();
						array[3] = true;
					}
					else
					{
						base.UnknownNode(accountType, "DeltaSyncV2::Name, DeltaSyncV2::Status, DeltaSyncV2::Fault, DeltaSyncV2::PartnerID");
					}
				}
				else
				{
					base.UnknownNode(accountType, "DeltaSyncV2::Name, DeltaSyncV2::Status, DeltaSyncV2::Fault, DeltaSyncV2::PartnerID");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return accountType;
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x0006B758 File Offset: 0x00069958
		private Fault Read2_Fault(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_DeltaSyncV2))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			Fault fault = new Fault();
			bool[] array = new bool[3];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(fault);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return fault;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id14_Faultcode && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
					{
						fault.Faultcode = base.Reader.ReadElementString();
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id15_Faultstring && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
					{
						fault.Faultstring = base.Reader.ReadElementString();
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id16_Detail && base.Reader.NamespaceURI == this.id2_DeltaSyncV2)
					{
						fault.Detail = base.Reader.ReadElementString();
						array[2] = true;
					}
					else
					{
						base.UnknownNode(fault, "DeltaSyncV2::Faultcode, DeltaSyncV2::Faultstring, DeltaSyncV2::Detail");
					}
				}
				else
				{
					base.UnknownNode(fault, "DeltaSyncV2::Faultcode, DeltaSyncV2::Faultstring, DeltaSyncV2::Detail");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return fault;
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x0006B966 File Offset: 0x00069B66
		protected override void InitCallbacks()
		{
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x0006B968 File Offset: 0x00069B68
		protected override void InitIDs()
		{
			this.id12_Name = base.Reader.NameTable.Add("Name");
			this.id4_Status = base.Reader.NameTable.Add("Status");
			this.id1_Provision = base.Reader.NameTable.Add("Provision");
			this.id16_Detail = base.Reader.NameTable.Add("Detail");
			this.id11_AccountType = base.Reader.NameTable.Add("AccountType");
			this.id5_Fault = base.Reader.NameTable.Add("Fault");
			this.id10_Read = base.Reader.NameTable.Add("Read");
			this.id3_Item = base.Reader.NameTable.Add("");
			this.id13_PartnerID = base.Reader.NameTable.Add("PartnerID");
			this.id14_Faultcode = base.Reader.NameTable.Add("Faultcode");
			this.id15_Faultstring = base.Reader.NameTable.Add("Faultstring");
			this.id8_Account = base.Reader.NameTable.Add("Account");
			this.id9_Delete = base.Reader.NameTable.Add("Delete");
			this.id6_Responses = base.Reader.NameTable.Add("Responses");
			this.id2_DeltaSyncV2 = base.Reader.NameTable.Add("DeltaSyncV2:");
			this.id7_Add = base.Reader.NameTable.Add("Add");
		}

		// Token: 0x0400290E RID: 10510
		private string id12_Name;

		// Token: 0x0400290F RID: 10511
		private string id4_Status;

		// Token: 0x04002910 RID: 10512
		private string id1_Provision;

		// Token: 0x04002911 RID: 10513
		private string id16_Detail;

		// Token: 0x04002912 RID: 10514
		private string id11_AccountType;

		// Token: 0x04002913 RID: 10515
		private string id5_Fault;

		// Token: 0x04002914 RID: 10516
		private string id10_Read;

		// Token: 0x04002915 RID: 10517
		private string id3_Item;

		// Token: 0x04002916 RID: 10518
		private string id13_PartnerID;

		// Token: 0x04002917 RID: 10519
		private string id14_Faultcode;

		// Token: 0x04002918 RID: 10520
		private string id15_Faultstring;

		// Token: 0x04002919 RID: 10521
		private string id8_Account;

		// Token: 0x0400291A RID: 10522
		private string id9_Delete;

		// Token: 0x0400291B RID: 10523
		private string id6_Responses;

		// Token: 0x0400291C RID: 10524
		private string id2_DeltaSyncV2;

		// Token: 0x0400291D RID: 10525
		private string id7_Add;
	}
}
