using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EdgeSync.Common
{
	// Token: 0x02000032 RID: 50
	internal class XmlSerializationReaderStatus : XmlSerializationReader
	{
		// Token: 0x06000122 RID: 290 RVA: 0x000066D0 File Offset: 0x000048D0
		public object Read5_Status()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_Status || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = this.Read4_Status(true, true);
			}
			else
			{
				base.UnknownNode(null, ":Status");
			}
			return result;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006740 File Offset: 0x00004940
		private Status Read4_Status(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id1_Status || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			Status status = new Status();
			bool[] array = new bool[11];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(status);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return status;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id3_Result && base.Reader.NamespaceURI == this.id2_Item)
					{
						status.Result = this.Read1_StatusResult(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id4_Type && base.Reader.NamespaceURI == this.id2_Item)
					{
						status.Type = this.Read2_SyncTreeType(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id5_Name && base.Reader.NamespaceURI == this.id2_Item)
					{
						status.Name = base.Reader.ReadElementString();
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id6_FailureDetails && base.Reader.NamespaceURI == this.id2_Item)
					{
						status.FailureDetails = base.Reader.ReadElementString();
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id7_StartUTC && base.Reader.NamespaceURI == this.id2_Item)
					{
						status.StartUTC = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
						array[4] = true;
					}
					else if (!array[5] && base.Reader.LocalName == this.id8_EndUTC && base.Reader.NamespaceURI == this.id2_Item)
					{
						status.EndUTC = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
						array[5] = true;
					}
					else if (!array[6] && base.Reader.LocalName == this.id9_Added && base.Reader.NamespaceURI == this.id2_Item)
					{
						status.Added = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[6] = true;
					}
					else if (!array[7] && base.Reader.LocalName == this.id10_Deleted && base.Reader.NamespaceURI == this.id2_Item)
					{
						status.Deleted = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[7] = true;
					}
					else if (!array[8] && base.Reader.LocalName == this.id11_Updated && base.Reader.NamespaceURI == this.id2_Item)
					{
						status.Updated = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[8] = true;
					}
					else if (!array[9] && base.Reader.LocalName == this.id12_Scanned && base.Reader.NamespaceURI == this.id2_Item)
					{
						status.Scanned = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[9] = true;
					}
					else if (!array[10] && base.Reader.LocalName == this.id13_TargetScanned && base.Reader.NamespaceURI == this.id2_Item)
					{
						status.TargetScanned = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[10] = true;
					}
					else
					{
						base.UnknownNode(status, ":Result, :Type, :Name, :FailureDetails, :StartUTC, :EndUTC, :Added, :Deleted, :Updated, :Scanned, :TargetScanned");
					}
				}
				else
				{
					base.UnknownNode(status, ":Result, :Type, :Name, :FailureDetails, :StartUTC, :EndUTC, :Added, :Deleted, :Updated, :Scanned, :TargetScanned");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return status;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00006BAC File Offset: 0x00004DAC
		internal Hashtable SyncTreeTypeValues
		{
			get
			{
				if (this._SyncTreeTypeValues == null)
				{
					this._SyncTreeTypeValues = new Hashtable
					{
						{
							"Configuration",
							1L
						},
						{
							"Recipients",
							2L
						},
						{
							"General",
							4L
						}
					};
				}
				return this._SyncTreeTypeValues;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006C0A File Offset: 0x00004E0A
		private SyncTreeType Read2_SyncTreeType(string s)
		{
			return (SyncTreeType)XmlSerializationReader.ToEnum(s, this.SyncTreeTypeValues, "global::Microsoft.Exchange.EdgeSync.Common.SyncTreeType");
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006C20 File Offset: 0x00004E20
		private StatusResult Read1_StatusResult(string s)
		{
			switch (s)
			{
			case "InProgress":
				return StatusResult.InProgress;
			case "Success":
				return StatusResult.Success;
			case "Aborted":
				return StatusResult.Aborted;
			case "CouldNotConnect":
				return StatusResult.CouldNotConnect;
			case "CouldNotLease":
				return StatusResult.CouldNotLease;
			case "LostLease":
				return StatusResult.LostLease;
			case "Incomplete":
				return StatusResult.Incomplete;
			case "NoSubscriptions":
				return StatusResult.NoSubscriptions;
			case "SyncDisabled":
				return StatusResult.SyncDisabled;
			}
			throw base.CreateUnknownConstantException(s, typeof(StatusResult));
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006D18 File Offset: 0x00004F18
		protected override void InitCallbacks()
		{
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006D1C File Offset: 0x00004F1C
		protected override void InitIDs()
		{
			this.id7_StartUTC = base.Reader.NameTable.Add("StartUTC");
			this.id5_Name = base.Reader.NameTable.Add("Name");
			this.id1_Status = base.Reader.NameTable.Add("Status");
			this.id12_Scanned = base.Reader.NameTable.Add("Scanned");
			this.id6_FailureDetails = base.Reader.NameTable.Add("FailureDetails");
			this.id13_TargetScanned = base.Reader.NameTable.Add("TargetScanned");
			this.id9_Added = base.Reader.NameTable.Add("Added");
			this.id2_Item = base.Reader.NameTable.Add("");
			this.id8_EndUTC = base.Reader.NameTable.Add("EndUTC");
			this.id3_Result = base.Reader.NameTable.Add("Result");
			this.id10_Deleted = base.Reader.NameTable.Add("Deleted");
			this.id4_Type = base.Reader.NameTable.Add("Type");
			this.id11_Updated = base.Reader.NameTable.Add("Updated");
		}

		// Token: 0x040000E1 RID: 225
		private Hashtable _SyncTreeTypeValues;

		// Token: 0x040000E2 RID: 226
		private string id7_StartUTC;

		// Token: 0x040000E3 RID: 227
		private string id5_Name;

		// Token: 0x040000E4 RID: 228
		private string id1_Status;

		// Token: 0x040000E5 RID: 229
		private string id12_Scanned;

		// Token: 0x040000E6 RID: 230
		private string id6_FailureDetails;

		// Token: 0x040000E7 RID: 231
		private string id13_TargetScanned;

		// Token: 0x040000E8 RID: 232
		private string id9_Added;

		// Token: 0x040000E9 RID: 233
		private string id2_Item;

		// Token: 0x040000EA RID: 234
		private string id8_EndUTC;

		// Token: 0x040000EB RID: 235
		private string id3_Result;

		// Token: 0x040000EC RID: 236
		private string id10_Deleted;

		// Token: 0x040000ED RID: 237
		private string id4_Type;

		// Token: 0x040000EE RID: 238
		private string id11_Updated;
	}
}
