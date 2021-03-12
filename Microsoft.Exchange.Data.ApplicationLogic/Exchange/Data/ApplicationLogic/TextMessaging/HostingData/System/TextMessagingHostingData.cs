using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.HostingData.System
{
	// Token: 0x02000041 RID: 65
	[HelpKeyword("vs.data.DataSet")]
	[XmlSchemaProvider("GetTypedDataSetSchema")]
	[DesignerCategory("code")]
	[XmlRoot("TextMessagingHostingData")]
	[ToolboxItem(true)]
	[Serializable]
	internal class TextMessagingHostingData : DataSet
	{
		// Token: 0x06000321 RID: 801 RVA: 0x0000C1F4 File Offset: 0x0000A3F4
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public TextMessagingHostingData()
		{
			base.BeginInit();
			this.InitClass();
			CollectionChangeEventHandler value = new CollectionChangeEventHandler(this.SchemaChanged);
			base.Tables.CollectionChanged += value;
			base.Relations.CollectionChanged += value;
			base.EndInit();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000C248 File Offset: 0x0000A448
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected TextMessagingHostingData(SerializationInfo info, StreamingContext context) : base(info, context, false)
		{
			if (base.IsBinarySerialized(info, context))
			{
				this.InitVars(false);
				CollectionChangeEventHandler value = new CollectionChangeEventHandler(this.SchemaChanged);
				this.Tables.CollectionChanged += value;
				this.Relations.CollectionChanged += value;
				return;
			}
			string s = (string)info.GetValue("XmlSchema", typeof(string));
			if (base.DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
			{
				DataSet dataSet = new DataSet();
				dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
				if (dataSet.Tables["_locDefinition"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData._locDefinitionDataTable(dataSet.Tables["_locDefinition"]));
				}
				if (dataSet.Tables["Regions"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.RegionsDataTable(dataSet.Tables["Regions"]));
				}
				if (dataSet.Tables["Region"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.RegionDataTable(dataSet.Tables["Region"]));
				}
				if (dataSet.Tables["Carriers"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.CarriersDataTable(dataSet.Tables["Carriers"]));
				}
				if (dataSet.Tables["Carrier"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.CarrierDataTable(dataSet.Tables["Carrier"]));
				}
				if (dataSet.Tables["LocalizedInfo"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.LocalizedInfoDataTable(dataSet.Tables["LocalizedInfo"]));
				}
				if (dataSet.Tables["Services"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.ServicesDataTable(dataSet.Tables["Services"]));
				}
				if (dataSet.Tables["Service"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.ServiceDataTable(dataSet.Tables["Service"]));
				}
				if (dataSet.Tables["VoiceCallForwarding"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.VoiceCallForwardingDataTable(dataSet.Tables["VoiceCallForwarding"]));
				}
				if (dataSet.Tables["SmtpToSmsGateway"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.SmtpToSmsGatewayDataTable(dataSet.Tables["SmtpToSmsGateway"]));
				}
				if (dataSet.Tables["RecipientAddressing"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.RecipientAddressingDataTable(dataSet.Tables["RecipientAddressing"]));
				}
				if (dataSet.Tables["MessageRendering"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.MessageRenderingDataTable(dataSet.Tables["MessageRendering"]));
				}
				if (dataSet.Tables["Capacity"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.CapacityDataTable(dataSet.Tables["Capacity"]));
				}
				base.DataSetName = dataSet.DataSetName;
				base.Prefix = dataSet.Prefix;
				base.Namespace = dataSet.Namespace;
				base.Locale = dataSet.Locale;
				base.CaseSensitive = dataSet.CaseSensitive;
				base.EnforceConstraints = dataSet.EnforceConstraints;
				base.Merge(dataSet, false, MissingSchemaAction.Add);
				this.InitVars();
			}
			else
			{
				base.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
			}
			base.GetSerializationData(info, context);
			CollectionChangeEventHandler value2 = new CollectionChangeEventHandler(this.SchemaChanged);
			base.Tables.CollectionChanged += value2;
			this.Relations.CollectionChanged += value2;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000C5FD File Offset: 0x0000A7FD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[Browsable(false)]
		public TextMessagingHostingData._locDefinitionDataTable _locDefinition
		{
			get
			{
				return this.table_locDefinition;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000C605 File Offset: 0x0000A805
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DebuggerNonUserCode]
		public TextMessagingHostingData.RegionsDataTable Regions
		{
			get
			{
				return this.tableRegions;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000C60D File Offset: 0x0000A80D
		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[Browsable(false)]
		public TextMessagingHostingData.RegionDataTable Region
		{
			get
			{
				return this.tableRegion;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000C615 File Offset: 0x0000A815
		[Browsable(false)]
		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public TextMessagingHostingData.CarriersDataTable Carriers
		{
			get
			{
				return this.tableCarriers;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000C61D File Offset: 0x0000A81D
		[Browsable(false)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DebuggerNonUserCode]
		public TextMessagingHostingData.CarrierDataTable Carrier
		{
			get
			{
				return this.tableCarrier;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000C625 File Offset: 0x0000A825
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public TextMessagingHostingData.LocalizedInfoDataTable LocalizedInfo
		{
			get
			{
				return this.tableLocalizedInfo;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000C62D File Offset: 0x0000A82D
		[Browsable(false)]
		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public TextMessagingHostingData.ServicesDataTable Services
		{
			get
			{
				return this.tableServices;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000C635 File Offset: 0x0000A835
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public TextMessagingHostingData.ServiceDataTable Service
		{
			get
			{
				return this.tableService;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000C63D File Offset: 0x0000A83D
		[Browsable(false)]
		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public TextMessagingHostingData.VoiceCallForwardingDataTable VoiceCallForwarding
		{
			get
			{
				return this.tableVoiceCallForwarding;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000C645 File Offset: 0x0000A845
		[DebuggerNonUserCode]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public TextMessagingHostingData.SmtpToSmsGatewayDataTable SmtpToSmsGateway
		{
			get
			{
				return this.tableSmtpToSmsGateway;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000C64D File Offset: 0x0000A84D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public TextMessagingHostingData.RecipientAddressingDataTable RecipientAddressing
		{
			get
			{
				return this.tableRecipientAddressing;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000C655 File Offset: 0x0000A855
		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[Browsable(false)]
		public TextMessagingHostingData.MessageRenderingDataTable MessageRendering
		{
			get
			{
				return this.tableMessageRendering;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000C65D File Offset: 0x0000A85D
		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[Browsable(false)]
		public TextMessagingHostingData.CapacityDataTable Capacity
		{
			get
			{
				return this.tableCapacity;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000C665 File Offset: 0x0000A865
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000C66D File Offset: 0x0000A86D
		[DebuggerNonUserCode]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public override SchemaSerializationMode SchemaSerializationMode
		{
			get
			{
				return this._schemaSerializationMode;
			}
			set
			{
				this._schemaSerializationMode = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000C676 File Offset: 0x0000A876
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public new DataTableCollection Tables
		{
			get
			{
				return base.Tables;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000C67E File Offset: 0x0000A87E
		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public new DataRelationCollection Relations
		{
			get
			{
				return base.Relations;
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000C686 File Offset: 0x0000A886
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void InitializeDerivedDataSet()
		{
			base.BeginInit();
			this.InitClass();
			base.EndInit();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000C69C File Offset: 0x0000A89C
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public override DataSet Clone()
		{
			TextMessagingHostingData textMessagingHostingData = (TextMessagingHostingData)base.Clone();
			textMessagingHostingData.InitVars();
			textMessagingHostingData.SchemaSerializationMode = this.SchemaSerializationMode;
			return textMessagingHostingData;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected override bool ShouldSerializeTables()
		{
			return false;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000C6CB File Offset: 0x0000A8CB
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override bool ShouldSerializeRelations()
		{
			return false;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected override void ReadXmlSerializable(XmlReader reader)
		{
			if (base.DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
			{
				this.Reset();
				DataSet dataSet = new DataSet();
				dataSet.ReadXml(reader);
				if (dataSet.Tables["_locDefinition"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData._locDefinitionDataTable(dataSet.Tables["_locDefinition"]));
				}
				if (dataSet.Tables["Regions"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.RegionsDataTable(dataSet.Tables["Regions"]));
				}
				if (dataSet.Tables["Region"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.RegionDataTable(dataSet.Tables["Region"]));
				}
				if (dataSet.Tables["Carriers"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.CarriersDataTable(dataSet.Tables["Carriers"]));
				}
				if (dataSet.Tables["Carrier"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.CarrierDataTable(dataSet.Tables["Carrier"]));
				}
				if (dataSet.Tables["LocalizedInfo"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.LocalizedInfoDataTable(dataSet.Tables["LocalizedInfo"]));
				}
				if (dataSet.Tables["Services"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.ServicesDataTable(dataSet.Tables["Services"]));
				}
				if (dataSet.Tables["Service"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.ServiceDataTable(dataSet.Tables["Service"]));
				}
				if (dataSet.Tables["VoiceCallForwarding"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.VoiceCallForwardingDataTable(dataSet.Tables["VoiceCallForwarding"]));
				}
				if (dataSet.Tables["SmtpToSmsGateway"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.SmtpToSmsGatewayDataTable(dataSet.Tables["SmtpToSmsGateway"]));
				}
				if (dataSet.Tables["RecipientAddressing"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.RecipientAddressingDataTable(dataSet.Tables["RecipientAddressing"]));
				}
				if (dataSet.Tables["MessageRendering"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.MessageRenderingDataTable(dataSet.Tables["MessageRendering"]));
				}
				if (dataSet.Tables["Capacity"] != null)
				{
					base.Tables.Add(new TextMessagingHostingData.CapacityDataTable(dataSet.Tables["Capacity"]));
				}
				base.DataSetName = dataSet.DataSetName;
				base.Prefix = dataSet.Prefix;
				base.Namespace = dataSet.Namespace;
				base.Locale = dataSet.Locale;
				base.CaseSensitive = dataSet.CaseSensitive;
				base.EnforceConstraints = dataSet.EnforceConstraints;
				base.Merge(dataSet, false, MissingSchemaAction.Add);
				this.InitVars();
				return;
			}
			base.ReadXml(reader);
			this.InitVars();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected override XmlSchema GetSchemaSerializable()
		{
			MemoryStream memoryStream = new MemoryStream();
			base.WriteXmlSchema(new XmlTextWriter(memoryStream, null));
			memoryStream.Position = 0L;
			return XmlSchema.Read(new XmlTextReader(memoryStream), null);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000CA24 File Offset: 0x0000AC24
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal void InitVars()
		{
			this.InitVars(true);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000CA30 File Offset: 0x0000AC30
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal void InitVars(bool initTable)
		{
			this.table_locDefinition = (TextMessagingHostingData._locDefinitionDataTable)base.Tables["_locDefinition"];
			if (initTable && this.table_locDefinition != null)
			{
				this.table_locDefinition.InitVars();
			}
			this.tableRegions = (TextMessagingHostingData.RegionsDataTable)base.Tables["Regions"];
			if (initTable && this.tableRegions != null)
			{
				this.tableRegions.InitVars();
			}
			this.tableRegion = (TextMessagingHostingData.RegionDataTable)base.Tables["Region"];
			if (initTable && this.tableRegion != null)
			{
				this.tableRegion.InitVars();
			}
			this.tableCarriers = (TextMessagingHostingData.CarriersDataTable)base.Tables["Carriers"];
			if (initTable && this.tableCarriers != null)
			{
				this.tableCarriers.InitVars();
			}
			this.tableCarrier = (TextMessagingHostingData.CarrierDataTable)base.Tables["Carrier"];
			if (initTable && this.tableCarrier != null)
			{
				this.tableCarrier.InitVars();
			}
			this.tableLocalizedInfo = (TextMessagingHostingData.LocalizedInfoDataTable)base.Tables["LocalizedInfo"];
			if (initTable && this.tableLocalizedInfo != null)
			{
				this.tableLocalizedInfo.InitVars();
			}
			this.tableServices = (TextMessagingHostingData.ServicesDataTable)base.Tables["Services"];
			if (initTable && this.tableServices != null)
			{
				this.tableServices.InitVars();
			}
			this.tableService = (TextMessagingHostingData.ServiceDataTable)base.Tables["Service"];
			if (initTable && this.tableService != null)
			{
				this.tableService.InitVars();
			}
			this.tableVoiceCallForwarding = (TextMessagingHostingData.VoiceCallForwardingDataTable)base.Tables["VoiceCallForwarding"];
			if (initTable && this.tableVoiceCallForwarding != null)
			{
				this.tableVoiceCallForwarding.InitVars();
			}
			this.tableSmtpToSmsGateway = (TextMessagingHostingData.SmtpToSmsGatewayDataTable)base.Tables["SmtpToSmsGateway"];
			if (initTable && this.tableSmtpToSmsGateway != null)
			{
				this.tableSmtpToSmsGateway.InitVars();
			}
			this.tableRecipientAddressing = (TextMessagingHostingData.RecipientAddressingDataTable)base.Tables["RecipientAddressing"];
			if (initTable && this.tableRecipientAddressing != null)
			{
				this.tableRecipientAddressing.InitVars();
			}
			this.tableMessageRendering = (TextMessagingHostingData.MessageRenderingDataTable)base.Tables["MessageRendering"];
			if (initTable && this.tableMessageRendering != null)
			{
				this.tableMessageRendering.InitVars();
			}
			this.tableCapacity = (TextMessagingHostingData.CapacityDataTable)base.Tables["Capacity"];
			if (initTable && this.tableCapacity != null)
			{
				this.tableCapacity.InitVars();
			}
			this.relationRegions_Region = this.Relations["Regions_Region"];
			this.relationCarriers_Carrier = this.Relations["Carriers_Carrier"];
			this.relationCarrier_LocalizedInfo = this.Relations["Carrier_LocalizedInfo"];
			this.relationFK_Services_Service = this.Relations["FK_Services_Service"];
			this.relationFK_Carrier_Service = this.Relations["FK_Carrier_Service"];
			this.relationFK_Region_Service = this.Relations["FK_Region_Service"];
			this.relationFK_Service_VoiceCallForwarding = this.Relations["FK_Service_VoiceCallForwarding"];
			this.relationFK_Service_SmtpToSmsGateway = this.Relations["FK_Service_SmtpToSmsGateway"];
			this.relationFK_SmtpToSmsGateway_RecipientAddressing = this.Relations["FK_SmtpToSmsGateway_RecipientAddressing"];
			this.relationFK_SmtpToSmsGateway_MessageRendering = this.Relations["FK_SmtpToSmsGateway_MessageRendering"];
			this.relationFK_MessageRendering_Capacity = this.Relations["FK_MessageRendering_Capacity"];
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private void InitClass()
		{
			base.DataSetName = "TextMessagingHostingData";
			base.Prefix = "";
			base.Locale = new CultureInfo("");
			base.EnforceConstraints = true;
			this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			this.table_locDefinition = new TextMessagingHostingData._locDefinitionDataTable();
			base.Tables.Add(this.table_locDefinition);
			this.tableRegions = new TextMessagingHostingData.RegionsDataTable();
			base.Tables.Add(this.tableRegions);
			this.tableRegion = new TextMessagingHostingData.RegionDataTable();
			base.Tables.Add(this.tableRegion);
			this.tableCarriers = new TextMessagingHostingData.CarriersDataTable();
			base.Tables.Add(this.tableCarriers);
			this.tableCarrier = new TextMessagingHostingData.CarrierDataTable();
			base.Tables.Add(this.tableCarrier);
			this.tableLocalizedInfo = new TextMessagingHostingData.LocalizedInfoDataTable();
			base.Tables.Add(this.tableLocalizedInfo);
			this.tableServices = new TextMessagingHostingData.ServicesDataTable();
			base.Tables.Add(this.tableServices);
			this.tableService = new TextMessagingHostingData.ServiceDataTable();
			base.Tables.Add(this.tableService);
			this.tableVoiceCallForwarding = new TextMessagingHostingData.VoiceCallForwardingDataTable();
			base.Tables.Add(this.tableVoiceCallForwarding);
			this.tableSmtpToSmsGateway = new TextMessagingHostingData.SmtpToSmsGatewayDataTable();
			base.Tables.Add(this.tableSmtpToSmsGateway);
			this.tableRecipientAddressing = new TextMessagingHostingData.RecipientAddressingDataTable();
			base.Tables.Add(this.tableRecipientAddressing);
			this.tableMessageRendering = new TextMessagingHostingData.MessageRenderingDataTable();
			base.Tables.Add(this.tableMessageRendering);
			this.tableCapacity = new TextMessagingHostingData.CapacityDataTable();
			base.Tables.Add(this.tableCapacity);
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint("Regions_Region", new DataColumn[]
			{
				this.tableRegions.Regions_IdColumn
			}, new DataColumn[]
			{
				this.tableRegion.Regions_IdColumn
			});
			this.tableRegion.Constraints.Add(foreignKeyConstraint);
			foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
			foreignKeyConstraint.DeleteRule = Rule.Cascade;
			foreignKeyConstraint.UpdateRule = Rule.Cascade;
			foreignKeyConstraint = new ForeignKeyConstraint("Carriers_Carrier", new DataColumn[]
			{
				this.tableCarriers.Carriers_IdColumn
			}, new DataColumn[]
			{
				this.tableCarrier.Carriers_IdColumn
			});
			this.tableCarrier.Constraints.Add(foreignKeyConstraint);
			foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
			foreignKeyConstraint.DeleteRule = Rule.Cascade;
			foreignKeyConstraint.UpdateRule = Rule.Cascade;
			foreignKeyConstraint = new ForeignKeyConstraint("Carrier_LocalizedInfo", new DataColumn[]
			{
				this.tableCarrier.IdentityColumn
			}, new DataColumn[]
			{
				this.tableLocalizedInfo.CarrierIdentityColumn
			});
			this.tableLocalizedInfo.Constraints.Add(foreignKeyConstraint);
			foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
			foreignKeyConstraint.DeleteRule = Rule.Cascade;
			foreignKeyConstraint.UpdateRule = Rule.Cascade;
			foreignKeyConstraint = new ForeignKeyConstraint("FK_Services_Service", new DataColumn[]
			{
				this.tableServices.Services_IdColumn
			}, new DataColumn[]
			{
				this.tableService.Services_IdColumn
			});
			this.tableService.Constraints.Add(foreignKeyConstraint);
			foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
			foreignKeyConstraint.DeleteRule = Rule.Cascade;
			foreignKeyConstraint.UpdateRule = Rule.Cascade;
			foreignKeyConstraint = new ForeignKeyConstraint("FK_Carrier_Service", new DataColumn[]
			{
				this.tableCarrier.IdentityColumn
			}, new DataColumn[]
			{
				this.tableService.CarrierIdentityColumn
			});
			this.tableService.Constraints.Add(foreignKeyConstraint);
			foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
			foreignKeyConstraint.DeleteRule = Rule.Cascade;
			foreignKeyConstraint.UpdateRule = Rule.Cascade;
			foreignKeyConstraint = new ForeignKeyConstraint("FK_Region_Service", new DataColumn[]
			{
				this.tableRegion.Iso2Column
			}, new DataColumn[]
			{
				this.tableService.RegionIso2Column
			});
			this.tableService.Constraints.Add(foreignKeyConstraint);
			foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
			foreignKeyConstraint.DeleteRule = Rule.Cascade;
			foreignKeyConstraint.UpdateRule = Rule.Cascade;
			foreignKeyConstraint = new ForeignKeyConstraint("FK_Service_VoiceCallForwarding", new DataColumn[]
			{
				this.tableService.RegionIso2Column,
				this.tableService.CarrierIdentityColumn,
				this.tableService.TypeColumn
			}, new DataColumn[]
			{
				this.tableVoiceCallForwarding.RegionIso2Column,
				this.tableVoiceCallForwarding.CarrierIdentityColumn,
				this.tableVoiceCallForwarding.ServiceTypeColumn
			});
			this.tableVoiceCallForwarding.Constraints.Add(foreignKeyConstraint);
			foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
			foreignKeyConstraint.DeleteRule = Rule.Cascade;
			foreignKeyConstraint.UpdateRule = Rule.Cascade;
			foreignKeyConstraint = new ForeignKeyConstraint("FK_Service_SmtpToSmsGateway", new DataColumn[]
			{
				this.tableService.RegionIso2Column,
				this.tableService.CarrierIdentityColumn,
				this.tableService.TypeColumn
			}, new DataColumn[]
			{
				this.tableSmtpToSmsGateway.RegionIso2Column,
				this.tableSmtpToSmsGateway.CarrierIdentityColumn,
				this.tableSmtpToSmsGateway.ServiceTypeColumn
			});
			this.tableSmtpToSmsGateway.Constraints.Add(foreignKeyConstraint);
			foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
			foreignKeyConstraint.DeleteRule = Rule.Cascade;
			foreignKeyConstraint.UpdateRule = Rule.Cascade;
			foreignKeyConstraint = new ForeignKeyConstraint("FK_SmtpToSmsGateway_RecipientAddressing", new DataColumn[]
			{
				this.tableSmtpToSmsGateway.RegionIso2Column,
				this.tableSmtpToSmsGateway.CarrierIdentityColumn,
				this.tableSmtpToSmsGateway.ServiceTypeColumn
			}, new DataColumn[]
			{
				this.tableRecipientAddressing.RegionIso2Column,
				this.tableRecipientAddressing.CarrierIdentityColumn,
				this.tableRecipientAddressing.ServiceTypeColumn
			});
			this.tableRecipientAddressing.Constraints.Add(foreignKeyConstraint);
			foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
			foreignKeyConstraint.DeleteRule = Rule.Cascade;
			foreignKeyConstraint.UpdateRule = Rule.Cascade;
			foreignKeyConstraint = new ForeignKeyConstraint("FK_SmtpToSmsGateway_MessageRendering", new DataColumn[]
			{
				this.tableSmtpToSmsGateway.RegionIso2Column,
				this.tableSmtpToSmsGateway.CarrierIdentityColumn,
				this.tableSmtpToSmsGateway.ServiceTypeColumn
			}, new DataColumn[]
			{
				this.tableMessageRendering.RegionIso2Column,
				this.tableMessageRendering.CarrierIdentityColumn,
				this.tableMessageRendering.ServiceTypeColumn
			});
			this.tableMessageRendering.Constraints.Add(foreignKeyConstraint);
			foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
			foreignKeyConstraint.DeleteRule = Rule.Cascade;
			foreignKeyConstraint.UpdateRule = Rule.Cascade;
			foreignKeyConstraint = new ForeignKeyConstraint("FK_MessageRendering_Capacity", new DataColumn[]
			{
				this.tableMessageRendering.RegionIso2Column,
				this.tableMessageRendering.CarrierIdentityColumn,
				this.tableMessageRendering.ServiceTypeColumn
			}, new DataColumn[]
			{
				this.tableCapacity.RegionIso2Column,
				this.tableCapacity.CarrierIdentityColumn,
				this.tableCapacity.ServiceTypeColumn
			});
			this.tableCapacity.Constraints.Add(foreignKeyConstraint);
			foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
			foreignKeyConstraint.DeleteRule = Rule.Cascade;
			foreignKeyConstraint.UpdateRule = Rule.Cascade;
			this.relationRegions_Region = new DataRelation("Regions_Region", new DataColumn[]
			{
				this.tableRegions.Regions_IdColumn
			}, new DataColumn[]
			{
				this.tableRegion.Regions_IdColumn
			}, false);
			this.relationRegions_Region.Nested = true;
			this.Relations.Add(this.relationRegions_Region);
			this.relationCarriers_Carrier = new DataRelation("Carriers_Carrier", new DataColumn[]
			{
				this.tableCarriers.Carriers_IdColumn
			}, new DataColumn[]
			{
				this.tableCarrier.Carriers_IdColumn
			}, false);
			this.relationCarriers_Carrier.Nested = true;
			this.Relations.Add(this.relationCarriers_Carrier);
			this.relationCarrier_LocalizedInfo = new DataRelation("Carrier_LocalizedInfo", new DataColumn[]
			{
				this.tableCarrier.IdentityColumn
			}, new DataColumn[]
			{
				this.tableLocalizedInfo.CarrierIdentityColumn
			}, false);
			this.relationCarrier_LocalizedInfo.Nested = true;
			this.Relations.Add(this.relationCarrier_LocalizedInfo);
			this.relationFK_Services_Service = new DataRelation("FK_Services_Service", new DataColumn[]
			{
				this.tableServices.Services_IdColumn
			}, new DataColumn[]
			{
				this.tableService.Services_IdColumn
			}, false);
			this.relationFK_Services_Service.Nested = true;
			this.Relations.Add(this.relationFK_Services_Service);
			this.relationFK_Carrier_Service = new DataRelation("FK_Carrier_Service", new DataColumn[]
			{
				this.tableCarrier.IdentityColumn
			}, new DataColumn[]
			{
				this.tableService.CarrierIdentityColumn
			}, false);
			this.Relations.Add(this.relationFK_Carrier_Service);
			this.relationFK_Region_Service = new DataRelation("FK_Region_Service", new DataColumn[]
			{
				this.tableRegion.Iso2Column
			}, new DataColumn[]
			{
				this.tableService.RegionIso2Column
			}, false);
			this.Relations.Add(this.relationFK_Region_Service);
			this.relationFK_Service_VoiceCallForwarding = new DataRelation("FK_Service_VoiceCallForwarding", new DataColumn[]
			{
				this.tableService.RegionIso2Column,
				this.tableService.CarrierIdentityColumn,
				this.tableService.TypeColumn
			}, new DataColumn[]
			{
				this.tableVoiceCallForwarding.RegionIso2Column,
				this.tableVoiceCallForwarding.CarrierIdentityColumn,
				this.tableVoiceCallForwarding.ServiceTypeColumn
			}, false);
			this.relationFK_Service_VoiceCallForwarding.Nested = true;
			this.Relations.Add(this.relationFK_Service_VoiceCallForwarding);
			this.relationFK_Service_SmtpToSmsGateway = new DataRelation("FK_Service_SmtpToSmsGateway", new DataColumn[]
			{
				this.tableService.RegionIso2Column,
				this.tableService.CarrierIdentityColumn,
				this.tableService.TypeColumn
			}, new DataColumn[]
			{
				this.tableSmtpToSmsGateway.RegionIso2Column,
				this.tableSmtpToSmsGateway.CarrierIdentityColumn,
				this.tableSmtpToSmsGateway.ServiceTypeColumn
			}, false);
			this.relationFK_Service_SmtpToSmsGateway.Nested = true;
			this.Relations.Add(this.relationFK_Service_SmtpToSmsGateway);
			this.relationFK_SmtpToSmsGateway_RecipientAddressing = new DataRelation("FK_SmtpToSmsGateway_RecipientAddressing", new DataColumn[]
			{
				this.tableSmtpToSmsGateway.RegionIso2Column,
				this.tableSmtpToSmsGateway.CarrierIdentityColumn,
				this.tableSmtpToSmsGateway.ServiceTypeColumn
			}, new DataColumn[]
			{
				this.tableRecipientAddressing.RegionIso2Column,
				this.tableRecipientAddressing.CarrierIdentityColumn,
				this.tableRecipientAddressing.ServiceTypeColumn
			}, false);
			this.relationFK_SmtpToSmsGateway_RecipientAddressing.Nested = true;
			this.Relations.Add(this.relationFK_SmtpToSmsGateway_RecipientAddressing);
			this.relationFK_SmtpToSmsGateway_MessageRendering = new DataRelation("FK_SmtpToSmsGateway_MessageRendering", new DataColumn[]
			{
				this.tableSmtpToSmsGateway.RegionIso2Column,
				this.tableSmtpToSmsGateway.CarrierIdentityColumn,
				this.tableSmtpToSmsGateway.ServiceTypeColumn
			}, new DataColumn[]
			{
				this.tableMessageRendering.RegionIso2Column,
				this.tableMessageRendering.CarrierIdentityColumn,
				this.tableMessageRendering.ServiceTypeColumn
			}, false);
			this.relationFK_SmtpToSmsGateway_MessageRendering.Nested = true;
			this.Relations.Add(this.relationFK_SmtpToSmsGateway_MessageRendering);
			this.relationFK_MessageRendering_Capacity = new DataRelation("FK_MessageRendering_Capacity", new DataColumn[]
			{
				this.tableMessageRendering.RegionIso2Column,
				this.tableMessageRendering.CarrierIdentityColumn,
				this.tableMessageRendering.ServiceTypeColumn
			}, new DataColumn[]
			{
				this.tableCapacity.RegionIso2Column,
				this.tableCapacity.CarrierIdentityColumn,
				this.tableCapacity.ServiceTypeColumn
			}, false);
			this.relationFK_MessageRendering_Capacity.Nested = true;
			this.Relations.Add(this.relationFK_MessageRendering_Capacity);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000D9F1 File Offset: 0x0000BBF1
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerialize_locDefinition()
		{
			return false;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private bool ShouldSerializeRegions()
		{
			return false;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000D9F7 File Offset: 0x0000BBF7
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private bool ShouldSerializeRegion()
		{
			return false;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000D9FA File Offset: 0x0000BBFA
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private bool ShouldSerializeCarriers()
		{
			return false;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000D9FD File Offset: 0x0000BBFD
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private bool ShouldSerializeCarrier()
		{
			return false;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000DA00 File Offset: 0x0000BC00
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializeLocalizedInfo()
		{
			return false;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000DA03 File Offset: 0x0000BC03
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private bool ShouldSerializeServices()
		{
			return false;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000DA06 File Offset: 0x0000BC06
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializeService()
		{
			return false;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000DA09 File Offset: 0x0000BC09
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializeVoiceCallForwarding()
		{
			return false;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000DA0C File Offset: 0x0000BC0C
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private bool ShouldSerializeSmtpToSmsGateway()
		{
			return false;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000DA0F File Offset: 0x0000BC0F
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializeRecipientAddressing()
		{
			return false;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000DA12 File Offset: 0x0000BC12
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private bool ShouldSerializeMessageRendering()
		{
			return false;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000DA15 File Offset: 0x0000BC15
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializeCapacity()
		{
			return false;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000DA18 File Offset: 0x0000BC18
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private void SchemaChanged(object sender, CollectionChangeEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Remove)
			{
				this.InitVars();
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000DA2C File Offset: 0x0000BC2C
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
		{
			TextMessagingHostingData textMessagingHostingData = new TextMessagingHostingData();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = textMessagingHostingData.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = textMessagingHostingData.GetSchemaSerializable();
			if (xs.Contains(schemaSerializable.TargetNamespace))
			{
				MemoryStream memoryStream = new MemoryStream();
				MemoryStream memoryStream2 = new MemoryStream();
				try
				{
					schemaSerializable.Write(memoryStream);
					foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
					{
						XmlSchema xmlSchema = (XmlSchema)obj;
						memoryStream2.SetLength(0L);
						xmlSchema.Write(memoryStream2);
						if (memoryStream.Length == memoryStream2.Length)
						{
							memoryStream.Position = 0L;
							memoryStream2.Position = 0L;
							while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
							{
							}
							if (memoryStream.Position == memoryStream.Length)
							{
								return xmlSchemaComplexType;
							}
						}
					}
				}
				finally
				{
					if (memoryStream != null)
					{
						memoryStream.Close();
					}
					if (memoryStream2 != null)
					{
						memoryStream2.Close();
					}
				}
			}
			xs.Add(schemaSerializable);
			return xmlSchemaComplexType;
		}

		// Token: 0x04000102 RID: 258
		private TextMessagingHostingData._locDefinitionDataTable table_locDefinition;

		// Token: 0x04000103 RID: 259
		private TextMessagingHostingData.RegionsDataTable tableRegions;

		// Token: 0x04000104 RID: 260
		private TextMessagingHostingData.RegionDataTable tableRegion;

		// Token: 0x04000105 RID: 261
		private TextMessagingHostingData.CarriersDataTable tableCarriers;

		// Token: 0x04000106 RID: 262
		private TextMessagingHostingData.CarrierDataTable tableCarrier;

		// Token: 0x04000107 RID: 263
		private TextMessagingHostingData.LocalizedInfoDataTable tableLocalizedInfo;

		// Token: 0x04000108 RID: 264
		private TextMessagingHostingData.ServicesDataTable tableServices;

		// Token: 0x04000109 RID: 265
		private TextMessagingHostingData.ServiceDataTable tableService;

		// Token: 0x0400010A RID: 266
		private TextMessagingHostingData.VoiceCallForwardingDataTable tableVoiceCallForwarding;

		// Token: 0x0400010B RID: 267
		private TextMessagingHostingData.SmtpToSmsGatewayDataTable tableSmtpToSmsGateway;

		// Token: 0x0400010C RID: 268
		private TextMessagingHostingData.RecipientAddressingDataTable tableRecipientAddressing;

		// Token: 0x0400010D RID: 269
		private TextMessagingHostingData.MessageRenderingDataTable tableMessageRendering;

		// Token: 0x0400010E RID: 270
		private TextMessagingHostingData.CapacityDataTable tableCapacity;

		// Token: 0x0400010F RID: 271
		private DataRelation relationRegions_Region;

		// Token: 0x04000110 RID: 272
		private DataRelation relationCarriers_Carrier;

		// Token: 0x04000111 RID: 273
		private DataRelation relationCarrier_LocalizedInfo;

		// Token: 0x04000112 RID: 274
		private DataRelation relationFK_Services_Service;

		// Token: 0x04000113 RID: 275
		private DataRelation relationFK_Carrier_Service;

		// Token: 0x04000114 RID: 276
		private DataRelation relationFK_Region_Service;

		// Token: 0x04000115 RID: 277
		private DataRelation relationFK_Service_VoiceCallForwarding;

		// Token: 0x04000116 RID: 278
		private DataRelation relationFK_Service_SmtpToSmsGateway;

		// Token: 0x04000117 RID: 279
		private DataRelation relationFK_SmtpToSmsGateway_RecipientAddressing;

		// Token: 0x04000118 RID: 280
		private DataRelation relationFK_SmtpToSmsGateway_MessageRendering;

		// Token: 0x04000119 RID: 281
		private DataRelation relationFK_MessageRendering_Capacity;

		// Token: 0x0400011A RID: 282
		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		// Token: 0x02000042 RID: 66
		// (Invoke) Token: 0x0600034D RID: 845
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void _locDefinitionRowChangeEventHandler(object sender, TextMessagingHostingData._locDefinitionRowChangeEvent e);

		// Token: 0x02000043 RID: 67
		// (Invoke) Token: 0x06000351 RID: 849
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void RegionsRowChangeEventHandler(object sender, TextMessagingHostingData.RegionsRowChangeEvent e);

		// Token: 0x02000044 RID: 68
		// (Invoke) Token: 0x06000355 RID: 853
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void RegionRowChangeEventHandler(object sender, TextMessagingHostingData.RegionRowChangeEvent e);

		// Token: 0x02000045 RID: 69
		// (Invoke) Token: 0x06000359 RID: 857
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void CarriersRowChangeEventHandler(object sender, TextMessagingHostingData.CarriersRowChangeEvent e);

		// Token: 0x02000046 RID: 70
		// (Invoke) Token: 0x0600035D RID: 861
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void CarrierRowChangeEventHandler(object sender, TextMessagingHostingData.CarrierRowChangeEvent e);

		// Token: 0x02000047 RID: 71
		// (Invoke) Token: 0x06000361 RID: 865
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void LocalizedInfoRowChangeEventHandler(object sender, TextMessagingHostingData.LocalizedInfoRowChangeEvent e);

		// Token: 0x02000048 RID: 72
		// (Invoke) Token: 0x06000365 RID: 869
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void ServicesRowChangeEventHandler(object sender, TextMessagingHostingData.ServicesRowChangeEvent e);

		// Token: 0x02000049 RID: 73
		// (Invoke) Token: 0x06000369 RID: 873
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void ServiceRowChangeEventHandler(object sender, TextMessagingHostingData.ServiceRowChangeEvent e);

		// Token: 0x0200004A RID: 74
		// (Invoke) Token: 0x0600036D RID: 877
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void VoiceCallForwardingRowChangeEventHandler(object sender, TextMessagingHostingData.VoiceCallForwardingRowChangeEvent e);

		// Token: 0x0200004B RID: 75
		// (Invoke) Token: 0x06000371 RID: 881
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void SmtpToSmsGatewayRowChangeEventHandler(object sender, TextMessagingHostingData.SmtpToSmsGatewayRowChangeEvent e);

		// Token: 0x0200004C RID: 76
		// (Invoke) Token: 0x06000375 RID: 885
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void RecipientAddressingRowChangeEventHandler(object sender, TextMessagingHostingData.RecipientAddressingRowChangeEvent e);

		// Token: 0x0200004D RID: 77
		// (Invoke) Token: 0x06000379 RID: 889
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void MessageRenderingRowChangeEventHandler(object sender, TextMessagingHostingData.MessageRenderingRowChangeEvent e);

		// Token: 0x0200004E RID: 78
		// (Invoke) Token: 0x0600037D RID: 893
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void CapacityRowChangeEventHandler(object sender, TextMessagingHostingData.CapacityRowChangeEvent e);

		// Token: 0x0200004F RID: 79
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class _locDefinitionDataTable : TypedTableBase<TextMessagingHostingData._locDefinitionRow>
		{
			// Token: 0x06000380 RID: 896 RVA: 0x0000DB74 File Offset: 0x0000BD74
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public _locDefinitionDataTable()
			{
				base.TableName = "_locDefinition";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x06000381 RID: 897 RVA: 0x0000DB9C File Offset: 0x0000BD9C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal _locDefinitionDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			// Token: 0x06000382 RID: 898 RVA: 0x0000DC44 File Offset: 0x0000BE44
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected _locDefinitionDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x06000383 RID: 899 RVA: 0x0000DC54 File Offset: 0x0000BE54
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn _locDefinition_IdColumn
			{
				get
				{
					return this.column_locDefinition_Id;
				}
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x06000384 RID: 900 RVA: 0x0000DC5C File Offset: 0x0000BE5C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn _locDefault_locColumn
			{
				get
				{
					return this.column_locDefault_loc;
				}
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x06000385 RID: 901 RVA: 0x0000DC64 File Offset: 0x0000BE64
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			[Browsable(false)]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			// Token: 0x170000EE RID: 238
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData._locDefinitionRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData._locDefinitionRow)base.Rows[index];
				}
			}

			// Token: 0x14000031 RID: 49
			// (add) Token: 0x06000387 RID: 903 RVA: 0x0000DC84 File Offset: 0x0000BE84
			// (remove) Token: 0x06000388 RID: 904 RVA: 0x0000DCBC File Offset: 0x0000BEBC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData._locDefinitionRowChangeEventHandler _locDefinitionRowChanging;

			// Token: 0x14000032 RID: 50
			// (add) Token: 0x06000389 RID: 905 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
			// (remove) Token: 0x0600038A RID: 906 RVA: 0x0000DD2C File Offset: 0x0000BF2C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData._locDefinitionRowChangeEventHandler _locDefinitionRowChanged;

			// Token: 0x14000033 RID: 51
			// (add) Token: 0x0600038B RID: 907 RVA: 0x0000DD64 File Offset: 0x0000BF64
			// (remove) Token: 0x0600038C RID: 908 RVA: 0x0000DD9C File Offset: 0x0000BF9C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData._locDefinitionRowChangeEventHandler _locDefinitionRowDeleting;

			// Token: 0x14000034 RID: 52
			// (add) Token: 0x0600038D RID: 909 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
			// (remove) Token: 0x0600038E RID: 910 RVA: 0x0000DE0C File Offset: 0x0000C00C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData._locDefinitionRowChangeEventHandler _locDefinitionRowDeleted;

			// Token: 0x0600038F RID: 911 RVA: 0x0000DE41 File Offset: 0x0000C041
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void Add_locDefinitionRow(TextMessagingHostingData._locDefinitionRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x06000390 RID: 912 RVA: 0x0000DE50 File Offset: 0x0000C050
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData._locDefinitionRow Add_locDefinitionRow(string _locDefault_loc)
			{
				TextMessagingHostingData._locDefinitionRow locDefinitionRow = (TextMessagingHostingData._locDefinitionRow)base.NewRow();
				object[] itemArray = new object[]
				{
					null,
					_locDefault_loc
				};
				locDefinitionRow.ItemArray = itemArray;
				base.Rows.Add(locDefinitionRow);
				return locDefinitionRow;
			}

			// Token: 0x06000391 RID: 913 RVA: 0x0000DE8C File Offset: 0x0000C08C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				TextMessagingHostingData._locDefinitionDataTable locDefinitionDataTable = (TextMessagingHostingData._locDefinitionDataTable)base.Clone();
				locDefinitionDataTable.InitVars();
				return locDefinitionDataTable;
			}

			// Token: 0x06000392 RID: 914 RVA: 0x0000DEAC File Offset: 0x0000C0AC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData._locDefinitionDataTable();
			}

			// Token: 0x06000393 RID: 915 RVA: 0x0000DEB3 File Offset: 0x0000C0B3
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.column_locDefinition_Id = base.Columns["_locDefinition_Id"];
				this.column_locDefault_loc = base.Columns["_locDefault_loc"];
			}

			// Token: 0x06000394 RID: 916 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			private void InitClass()
			{
				this.column_locDefinition_Id = new DataColumn("_locDefinition_Id", typeof(int), null, MappingType.Hidden);
				base.Columns.Add(this.column_locDefinition_Id);
				this.column_locDefault_loc = new DataColumn("_locDefault_loc", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.column_locDefault_loc);
				base.Constraints.Add(new UniqueConstraint("_locDefinitionConstraint1", new DataColumn[]
				{
					this.column_locDefinition_Id
				}, true));
				this.column_locDefinition_Id.AutoIncrement = true;
				this.column_locDefinition_Id.AllowDBNull = false;
				this.column_locDefinition_Id.Unique = true;
				this.column_locDefinition_Id.Namespace = "";
				this.column_locDefault_loc.AllowDBNull = false;
			}

			// Token: 0x06000395 RID: 917 RVA: 0x0000DFB2 File Offset: 0x0000C1B2
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData._locDefinitionRow New_locDefinitionRow()
			{
				return (TextMessagingHostingData._locDefinitionRow)base.NewRow();
			}

			// Token: 0x06000396 RID: 918 RVA: 0x0000DFBF File Offset: 0x0000C1BF
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData._locDefinitionRow(builder);
			}

			// Token: 0x06000397 RID: 919 RVA: 0x0000DFC7 File Offset: 0x0000C1C7
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData._locDefinitionRow);
			}

			// Token: 0x06000398 RID: 920 RVA: 0x0000DFD3 File Offset: 0x0000C1D3
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this._locDefinitionRowChanged != null)
				{
					this._locDefinitionRowChanged(this, new TextMessagingHostingData._locDefinitionRowChangeEvent((TextMessagingHostingData._locDefinitionRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000399 RID: 921 RVA: 0x0000E006 File Offset: 0x0000C206
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this._locDefinitionRowChanging != null)
				{
					this._locDefinitionRowChanging(this, new TextMessagingHostingData._locDefinitionRowChangeEvent((TextMessagingHostingData._locDefinitionRow)e.Row, e.Action));
				}
			}

			// Token: 0x0600039A RID: 922 RVA: 0x0000E039 File Offset: 0x0000C239
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this._locDefinitionRowDeleted != null)
				{
					this._locDefinitionRowDeleted(this, new TextMessagingHostingData._locDefinitionRowChangeEvent((TextMessagingHostingData._locDefinitionRow)e.Row, e.Action));
				}
			}

			// Token: 0x0600039B RID: 923 RVA: 0x0000E06C File Offset: 0x0000C26C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this._locDefinitionRowDeleting != null)
				{
					this._locDefinitionRowDeleting(this, new TextMessagingHostingData._locDefinitionRowChangeEvent((TextMessagingHostingData._locDefinitionRow)e.Row, e.Action));
				}
			}

			// Token: 0x0600039C RID: 924 RVA: 0x0000E09F File Offset: 0x0000C29F
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void Remove_locDefinitionRow(TextMessagingHostingData._locDefinitionRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x0600039D RID: 925 RVA: 0x0000E0B0 File Offset: 0x0000C2B0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TextMessagingHostingData textMessagingHostingData = new TextMessagingHostingData();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = textMessagingHostingData.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "_locDefinitionDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = textMessagingHostingData.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}

			// Token: 0x0400011B RID: 283
			private DataColumn column_locDefinition_Id;

			// Token: 0x0400011C RID: 284
			private DataColumn column_locDefault_loc;
		}

		// Token: 0x02000050 RID: 80
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class RegionsDataTable : TypedTableBase<TextMessagingHostingData.RegionsRow>
		{
			// Token: 0x0600039E RID: 926 RVA: 0x0000E2A8 File Offset: 0x0000C4A8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public RegionsDataTable()
			{
				base.TableName = "Regions";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x0600039F RID: 927 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal RegionsDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			// Token: 0x060003A0 RID: 928 RVA: 0x0000E378 File Offset: 0x0000C578
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected RegionsDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000E388 File Offset: 0x0000C588
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn Regions_IdColumn
			{
				get
				{
					return this.columnRegions_Id;
				}
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000E390 File Offset: 0x0000C590
			[DebuggerNonUserCode]
			[Browsable(false)]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			// Token: 0x170000F1 RID: 241
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RegionsRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.RegionsRow)base.Rows[index];
				}
			}

			// Token: 0x14000035 RID: 53
			// (add) Token: 0x060003A4 RID: 932 RVA: 0x0000E3B0 File Offset: 0x0000C5B0
			// (remove) Token: 0x060003A5 RID: 933 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionsRowChangeEventHandler RegionsRowChanging;

			// Token: 0x14000036 RID: 54
			// (add) Token: 0x060003A6 RID: 934 RVA: 0x0000E420 File Offset: 0x0000C620
			// (remove) Token: 0x060003A7 RID: 935 RVA: 0x0000E458 File Offset: 0x0000C658
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionsRowChangeEventHandler RegionsRowChanged;

			// Token: 0x14000037 RID: 55
			// (add) Token: 0x060003A8 RID: 936 RVA: 0x0000E490 File Offset: 0x0000C690
			// (remove) Token: 0x060003A9 RID: 937 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionsRowChangeEventHandler RegionsRowDeleting;

			// Token: 0x14000038 RID: 56
			// (add) Token: 0x060003AA RID: 938 RVA: 0x0000E500 File Offset: 0x0000C700
			// (remove) Token: 0x060003AB RID: 939 RVA: 0x0000E538 File Offset: 0x0000C738
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionsRowChangeEventHandler RegionsRowDeleted;

			// Token: 0x060003AC RID: 940 RVA: 0x0000E56D File Offset: 0x0000C76D
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void AddRegionsRow(TextMessagingHostingData.RegionsRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x060003AD RID: 941 RVA: 0x0000E57C File Offset: 0x0000C77C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RegionsRow AddRegionsRow()
			{
				TextMessagingHostingData.RegionsRow regionsRow = (TextMessagingHostingData.RegionsRow)base.NewRow();
				object[] array = new object[1];
				object[] itemArray = array;
				regionsRow.ItemArray = itemArray;
				base.Rows.Add(regionsRow);
				return regionsRow;
			}

			// Token: 0x060003AE RID: 942 RVA: 0x0000E5B4 File Offset: 0x0000C7B4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				TextMessagingHostingData.RegionsDataTable regionsDataTable = (TextMessagingHostingData.RegionsDataTable)base.Clone();
				regionsDataTable.InitVars();
				return regionsDataTable;
			}

			// Token: 0x060003AF RID: 943 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.RegionsDataTable();
			}

			// Token: 0x060003B0 RID: 944 RVA: 0x0000E5DB File Offset: 0x0000C7DB
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnRegions_Id = base.Columns["Regions_Id"];
			}

			// Token: 0x060003B1 RID: 945 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			private void InitClass()
			{
				this.columnRegions_Id = new DataColumn("Regions_Id", typeof(int), null, MappingType.Hidden);
				base.Columns.Add(this.columnRegions_Id);
				base.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[]
				{
					this.columnRegions_Id
				}, true));
				this.columnRegions_Id.AutoIncrement = true;
				this.columnRegions_Id.AllowDBNull = false;
				this.columnRegions_Id.Unique = true;
				this.columnRegions_Id.Namespace = "";
			}

			// Token: 0x060003B2 RID: 946 RVA: 0x0000E689 File Offset: 0x0000C889
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RegionsRow NewRegionsRow()
			{
				return (TextMessagingHostingData.RegionsRow)base.NewRow();
			}

			// Token: 0x060003B3 RID: 947 RVA: 0x0000E696 File Offset: 0x0000C896
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.RegionsRow(builder);
			}

			// Token: 0x060003B4 RID: 948 RVA: 0x0000E69E File Offset: 0x0000C89E
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.RegionsRow);
			}

			// Token: 0x060003B5 RID: 949 RVA: 0x0000E6AA File Offset: 0x0000C8AA
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.RegionsRowChanged != null)
				{
					this.RegionsRowChanged(this, new TextMessagingHostingData.RegionsRowChangeEvent((TextMessagingHostingData.RegionsRow)e.Row, e.Action));
				}
			}

			// Token: 0x060003B6 RID: 950 RVA: 0x0000E6DD File Offset: 0x0000C8DD
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.RegionsRowChanging != null)
				{
					this.RegionsRowChanging(this, new TextMessagingHostingData.RegionsRowChangeEvent((TextMessagingHostingData.RegionsRow)e.Row, e.Action));
				}
			}

			// Token: 0x060003B7 RID: 951 RVA: 0x0000E710 File Offset: 0x0000C910
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.RegionsRowDeleted != null)
				{
					this.RegionsRowDeleted(this, new TextMessagingHostingData.RegionsRowChangeEvent((TextMessagingHostingData.RegionsRow)e.Row, e.Action));
				}
			}

			// Token: 0x060003B8 RID: 952 RVA: 0x0000E743 File Offset: 0x0000C943
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.RegionsRowDeleting != null)
				{
					this.RegionsRowDeleting(this, new TextMessagingHostingData.RegionsRowChangeEvent((TextMessagingHostingData.RegionsRow)e.Row, e.Action));
				}
			}

			// Token: 0x060003B9 RID: 953 RVA: 0x0000E776 File Offset: 0x0000C976
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void RemoveRegionsRow(TextMessagingHostingData.RegionsRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x060003BA RID: 954 RVA: 0x0000E784 File Offset: 0x0000C984
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TextMessagingHostingData textMessagingHostingData = new TextMessagingHostingData();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = textMessagingHostingData.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "RegionsDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = textMessagingHostingData.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}

			// Token: 0x04000121 RID: 289
			private DataColumn columnRegions_Id;
		}

		// Token: 0x02000051 RID: 81
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class RegionDataTable : TypedTableBase<TextMessagingHostingData.RegionRow>
		{
			// Token: 0x060003BB RID: 955 RVA: 0x0000E97C File Offset: 0x0000CB7C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public RegionDataTable()
			{
				base.TableName = "Region";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x060003BC RID: 956 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal RegionDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			// Token: 0x060003BD RID: 957 RVA: 0x0000EA4C File Offset: 0x0000CC4C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected RegionDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x060003BE RID: 958 RVA: 0x0000EA5C File Offset: 0x0000CC5C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CountryCodeColumn
			{
				get
				{
					return this.columnCountryCode;
				}
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x060003BF RID: 959 RVA: 0x0000EA64 File Offset: 0x0000CC64
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn PhoneNumberExampleColumn
			{
				get
				{
					return this.columnPhoneNumberExample;
				}
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000EA6C File Offset: 0x0000CC6C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn Iso2Column
			{
				get
				{
					return this.columnIso2;
				}
			}

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000EA74 File Offset: 0x0000CC74
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn Regions_IdColumn
			{
				get
				{
					return this.columnRegions_Id;
				}
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000EA7C File Offset: 0x0000CC7C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[Browsable(false)]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			// Token: 0x170000F7 RID: 247
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RegionRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.RegionRow)base.Rows[index];
				}
			}

			// Token: 0x14000039 RID: 57
			// (add) Token: 0x060003C4 RID: 964 RVA: 0x0000EA9C File Offset: 0x0000CC9C
			// (remove) Token: 0x060003C5 RID: 965 RVA: 0x0000EAD4 File Offset: 0x0000CCD4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionRowChangeEventHandler RegionRowChanging;

			// Token: 0x1400003A RID: 58
			// (add) Token: 0x060003C6 RID: 966 RVA: 0x0000EB0C File Offset: 0x0000CD0C
			// (remove) Token: 0x060003C7 RID: 967 RVA: 0x0000EB44 File Offset: 0x0000CD44
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionRowChangeEventHandler RegionRowChanged;

			// Token: 0x1400003B RID: 59
			// (add) Token: 0x060003C8 RID: 968 RVA: 0x0000EB7C File Offset: 0x0000CD7C
			// (remove) Token: 0x060003C9 RID: 969 RVA: 0x0000EBB4 File Offset: 0x0000CDB4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionRowChangeEventHandler RegionRowDeleting;

			// Token: 0x1400003C RID: 60
			// (add) Token: 0x060003CA RID: 970 RVA: 0x0000EBEC File Offset: 0x0000CDEC
			// (remove) Token: 0x060003CB RID: 971 RVA: 0x0000EC24 File Offset: 0x0000CE24
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionRowChangeEventHandler RegionRowDeleted;

			// Token: 0x060003CC RID: 972 RVA: 0x0000EC59 File Offset: 0x0000CE59
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void AddRegionRow(TextMessagingHostingData.RegionRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x060003CD RID: 973 RVA: 0x0000EC68 File Offset: 0x0000CE68
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RegionRow AddRegionRow(string CountryCode, string PhoneNumberExample, string Iso2, TextMessagingHostingData.RegionsRow parentRegionsRowByRegions_Region)
			{
				TextMessagingHostingData.RegionRow regionRow = (TextMessagingHostingData.RegionRow)base.NewRow();
				object[] array = new object[4];
				array[0] = CountryCode;
				array[1] = PhoneNumberExample;
				array[2] = Iso2;
				object[] array2 = array;
				if (parentRegionsRowByRegions_Region != null)
				{
					array2[3] = parentRegionsRowByRegions_Region[0];
				}
				regionRow.ItemArray = array2;
				base.Rows.Add(regionRow);
				return regionRow;
			}

			// Token: 0x060003CE RID: 974 RVA: 0x0000ECBC File Offset: 0x0000CEBC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RegionRow FindByIso2(string Iso2)
			{
				return (TextMessagingHostingData.RegionRow)base.Rows.Find(new object[]
				{
					Iso2
				});
			}

			// Token: 0x060003CF RID: 975 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				TextMessagingHostingData.RegionDataTable regionDataTable = (TextMessagingHostingData.RegionDataTable)base.Clone();
				regionDataTable.InitVars();
				return regionDataTable;
			}

			// Token: 0x060003D0 RID: 976 RVA: 0x0000ED08 File Offset: 0x0000CF08
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.RegionDataTable();
			}

			// Token: 0x060003D1 RID: 977 RVA: 0x0000ED10 File Offset: 0x0000CF10
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnCountryCode = base.Columns["CountryCode"];
				this.columnPhoneNumberExample = base.Columns["PhoneNumberExample"];
				this.columnIso2 = base.Columns["Iso2"];
				this.columnRegions_Id = base.Columns["Regions_Id"];
			}

			// Token: 0x060003D2 RID: 978 RVA: 0x0000ED78 File Offset: 0x0000CF78
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			private void InitClass()
			{
				this.columnCountryCode = new DataColumn("CountryCode", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnCountryCode);
				this.columnPhoneNumberExample = new DataColumn("PhoneNumberExample", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnPhoneNumberExample);
				this.columnIso2 = new DataColumn("Iso2", typeof(string), null, MappingType.Attribute);
				base.Columns.Add(this.columnIso2);
				this.columnRegions_Id = new DataColumn("Regions_Id", typeof(int), null, MappingType.Hidden);
				base.Columns.Add(this.columnRegions_Id);
				base.Constraints.Add(new UniqueConstraint("RegionKey1", new DataColumn[]
				{
					this.columnIso2
				}, true));
				this.columnCountryCode.AllowDBNull = false;
				this.columnIso2.AllowDBNull = false;
				this.columnIso2.Unique = true;
				this.columnIso2.Namespace = "";
				this.columnRegions_Id.Namespace = "";
			}

			// Token: 0x060003D3 RID: 979 RVA: 0x0000EEA4 File Offset: 0x0000D0A4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RegionRow NewRegionRow()
			{
				return (TextMessagingHostingData.RegionRow)base.NewRow();
			}

			// Token: 0x060003D4 RID: 980 RVA: 0x0000EEB1 File Offset: 0x0000D0B1
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.RegionRow(builder);
			}

			// Token: 0x060003D5 RID: 981 RVA: 0x0000EEB9 File Offset: 0x0000D0B9
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.RegionRow);
			}

			// Token: 0x060003D6 RID: 982 RVA: 0x0000EEC5 File Offset: 0x0000D0C5
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.RegionRowChanged != null)
				{
					this.RegionRowChanged(this, new TextMessagingHostingData.RegionRowChangeEvent((TextMessagingHostingData.RegionRow)e.Row, e.Action));
				}
			}

			// Token: 0x060003D7 RID: 983 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.RegionRowChanging != null)
				{
					this.RegionRowChanging(this, new TextMessagingHostingData.RegionRowChangeEvent((TextMessagingHostingData.RegionRow)e.Row, e.Action));
				}
			}

			// Token: 0x060003D8 RID: 984 RVA: 0x0000EF2B File Offset: 0x0000D12B
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.RegionRowDeleted != null)
				{
					this.RegionRowDeleted(this, new TextMessagingHostingData.RegionRowChangeEvent((TextMessagingHostingData.RegionRow)e.Row, e.Action));
				}
			}

			// Token: 0x060003D9 RID: 985 RVA: 0x0000EF5E File Offset: 0x0000D15E
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.RegionRowDeleting != null)
				{
					this.RegionRowDeleting(this, new TextMessagingHostingData.RegionRowChangeEvent((TextMessagingHostingData.RegionRow)e.Row, e.Action));
				}
			}

			// Token: 0x060003DA RID: 986 RVA: 0x0000EF91 File Offset: 0x0000D191
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void RemoveRegionRow(TextMessagingHostingData.RegionRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x060003DB RID: 987 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TextMessagingHostingData textMessagingHostingData = new TextMessagingHostingData();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = textMessagingHostingData.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "RegionDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = textMessagingHostingData.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}

			// Token: 0x04000126 RID: 294
			private DataColumn columnCountryCode;

			// Token: 0x04000127 RID: 295
			private DataColumn columnPhoneNumberExample;

			// Token: 0x04000128 RID: 296
			private DataColumn columnIso2;

			// Token: 0x04000129 RID: 297
			private DataColumn columnRegions_Id;
		}

		// Token: 0x02000052 RID: 82
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class CarriersDataTable : TypedTableBase<TextMessagingHostingData.CarriersRow>
		{
			// Token: 0x060003DC RID: 988 RVA: 0x0000F198 File Offset: 0x0000D398
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public CarriersDataTable()
			{
				base.TableName = "Carriers";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x060003DD RID: 989 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal CarriersDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			// Token: 0x060003DE RID: 990 RVA: 0x0000F268 File Offset: 0x0000D468
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected CarriersDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x060003DF RID: 991 RVA: 0x0000F278 File Offset: 0x0000D478
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn Carriers_IdColumn
			{
				get
				{
					return this.columnCarriers_Id;
				}
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000F280 File Offset: 0x0000D480
			[Browsable(false)]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			// Token: 0x170000FA RID: 250
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CarriersRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.CarriersRow)base.Rows[index];
				}
			}

			// Token: 0x1400003D RID: 61
			// (add) Token: 0x060003E2 RID: 994 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
			// (remove) Token: 0x060003E3 RID: 995 RVA: 0x0000F2D8 File Offset: 0x0000D4D8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarriersRowChangeEventHandler CarriersRowChanging;

			// Token: 0x1400003E RID: 62
			// (add) Token: 0x060003E4 RID: 996 RVA: 0x0000F310 File Offset: 0x0000D510
			// (remove) Token: 0x060003E5 RID: 997 RVA: 0x0000F348 File Offset: 0x0000D548
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarriersRowChangeEventHandler CarriersRowChanged;

			// Token: 0x1400003F RID: 63
			// (add) Token: 0x060003E6 RID: 998 RVA: 0x0000F380 File Offset: 0x0000D580
			// (remove) Token: 0x060003E7 RID: 999 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarriersRowChangeEventHandler CarriersRowDeleting;

			// Token: 0x14000040 RID: 64
			// (add) Token: 0x060003E8 RID: 1000 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
			// (remove) Token: 0x060003E9 RID: 1001 RVA: 0x0000F428 File Offset: 0x0000D628
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarriersRowChangeEventHandler CarriersRowDeleted;

			// Token: 0x060003EA RID: 1002 RVA: 0x0000F45D File Offset: 0x0000D65D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void AddCarriersRow(TextMessagingHostingData.CarriersRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x060003EB RID: 1003 RVA: 0x0000F46C File Offset: 0x0000D66C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.CarriersRow AddCarriersRow()
			{
				TextMessagingHostingData.CarriersRow carriersRow = (TextMessagingHostingData.CarriersRow)base.NewRow();
				object[] array = new object[1];
				object[] itemArray = array;
				carriersRow.ItemArray = itemArray;
				base.Rows.Add(carriersRow);
				return carriersRow;
			}

			// Token: 0x060003EC RID: 1004 RVA: 0x0000F4A4 File Offset: 0x0000D6A4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				TextMessagingHostingData.CarriersDataTable carriersDataTable = (TextMessagingHostingData.CarriersDataTable)base.Clone();
				carriersDataTable.InitVars();
				return carriersDataTable;
			}

			// Token: 0x060003ED RID: 1005 RVA: 0x0000F4C4 File Offset: 0x0000D6C4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.CarriersDataTable();
			}

			// Token: 0x060003EE RID: 1006 RVA: 0x0000F4CB File Offset: 0x0000D6CB
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal void InitVars()
			{
				this.columnCarriers_Id = base.Columns["Carriers_Id"];
			}

			// Token: 0x060003EF RID: 1007 RVA: 0x0000F4E4 File Offset: 0x0000D6E4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			private void InitClass()
			{
				this.columnCarriers_Id = new DataColumn("Carriers_Id", typeof(int), null, MappingType.Hidden);
				base.Columns.Add(this.columnCarriers_Id);
				base.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[]
				{
					this.columnCarriers_Id
				}, true));
				this.columnCarriers_Id.AutoIncrement = true;
				this.columnCarriers_Id.AllowDBNull = false;
				this.columnCarriers_Id.Unique = true;
				this.columnCarriers_Id.Namespace = "";
			}

			// Token: 0x060003F0 RID: 1008 RVA: 0x0000F579 File Offset: 0x0000D779
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.CarriersRow NewCarriersRow()
			{
				return (TextMessagingHostingData.CarriersRow)base.NewRow();
			}

			// Token: 0x060003F1 RID: 1009 RVA: 0x0000F586 File Offset: 0x0000D786
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.CarriersRow(builder);
			}

			// Token: 0x060003F2 RID: 1010 RVA: 0x0000F58E File Offset: 0x0000D78E
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.CarriersRow);
			}

			// Token: 0x060003F3 RID: 1011 RVA: 0x0000F59A File Offset: 0x0000D79A
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.CarriersRowChanged != null)
				{
					this.CarriersRowChanged(this, new TextMessagingHostingData.CarriersRowChangeEvent((TextMessagingHostingData.CarriersRow)e.Row, e.Action));
				}
			}

			// Token: 0x060003F4 RID: 1012 RVA: 0x0000F5CD File Offset: 0x0000D7CD
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.CarriersRowChanging != null)
				{
					this.CarriersRowChanging(this, new TextMessagingHostingData.CarriersRowChangeEvent((TextMessagingHostingData.CarriersRow)e.Row, e.Action));
				}
			}

			// Token: 0x060003F5 RID: 1013 RVA: 0x0000F600 File Offset: 0x0000D800
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.CarriersRowDeleted != null)
				{
					this.CarriersRowDeleted(this, new TextMessagingHostingData.CarriersRowChangeEvent((TextMessagingHostingData.CarriersRow)e.Row, e.Action));
				}
			}

			// Token: 0x060003F6 RID: 1014 RVA: 0x0000F633 File Offset: 0x0000D833
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.CarriersRowDeleting != null)
				{
					this.CarriersRowDeleting(this, new TextMessagingHostingData.CarriersRowChangeEvent((TextMessagingHostingData.CarriersRow)e.Row, e.Action));
				}
			}

			// Token: 0x060003F7 RID: 1015 RVA: 0x0000F666 File Offset: 0x0000D866
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void RemoveCarriersRow(TextMessagingHostingData.CarriersRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x060003F8 RID: 1016 RVA: 0x0000F674 File Offset: 0x0000D874
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TextMessagingHostingData textMessagingHostingData = new TextMessagingHostingData();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = textMessagingHostingData.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "CarriersDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = textMessagingHostingData.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}

			// Token: 0x0400012E RID: 302
			private DataColumn columnCarriers_Id;
		}

		// Token: 0x02000053 RID: 83
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class CarrierDataTable : TypedTableBase<TextMessagingHostingData.CarrierRow>
		{
			// Token: 0x060003F9 RID: 1017 RVA: 0x0000F86C File Offset: 0x0000DA6C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public CarrierDataTable()
			{
				base.TableName = "Carrier";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x060003FA RID: 1018 RVA: 0x0000F894 File Offset: 0x0000DA94
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal CarrierDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			// Token: 0x060003FB RID: 1019 RVA: 0x0000F93C File Offset: 0x0000DB3C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected CarrierDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000F94C File Offset: 0x0000DB4C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn IdentityColumn
			{
				get
				{
					return this.columnIdentity;
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000F954 File Offset: 0x0000DB54
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn Carriers_IdColumn
			{
				get
				{
					return this.columnCarriers_Id;
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000F95C File Offset: 0x0000DB5C
			[Browsable(false)]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			// Token: 0x170000FE RID: 254
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.CarrierRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.CarrierRow)base.Rows[index];
				}
			}

			// Token: 0x14000041 RID: 65
			// (add) Token: 0x06000400 RID: 1024 RVA: 0x0000F97C File Offset: 0x0000DB7C
			// (remove) Token: 0x06000401 RID: 1025 RVA: 0x0000F9B4 File Offset: 0x0000DBB4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarrierRowChangeEventHandler CarrierRowChanging;

			// Token: 0x14000042 RID: 66
			// (add) Token: 0x06000402 RID: 1026 RVA: 0x0000F9EC File Offset: 0x0000DBEC
			// (remove) Token: 0x06000403 RID: 1027 RVA: 0x0000FA24 File Offset: 0x0000DC24
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarrierRowChangeEventHandler CarrierRowChanged;

			// Token: 0x14000043 RID: 67
			// (add) Token: 0x06000404 RID: 1028 RVA: 0x0000FA5C File Offset: 0x0000DC5C
			// (remove) Token: 0x06000405 RID: 1029 RVA: 0x0000FA94 File Offset: 0x0000DC94
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarrierRowChangeEventHandler CarrierRowDeleting;

			// Token: 0x14000044 RID: 68
			// (add) Token: 0x06000406 RID: 1030 RVA: 0x0000FACC File Offset: 0x0000DCCC
			// (remove) Token: 0x06000407 RID: 1031 RVA: 0x0000FB04 File Offset: 0x0000DD04
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarrierRowChangeEventHandler CarrierRowDeleted;

			// Token: 0x06000408 RID: 1032 RVA: 0x0000FB39 File Offset: 0x0000DD39
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void AddCarrierRow(TextMessagingHostingData.CarrierRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x06000409 RID: 1033 RVA: 0x0000FB48 File Offset: 0x0000DD48
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CarrierRow AddCarrierRow(int Identity, TextMessagingHostingData.CarriersRow parentCarriersRowByCarriers_Carrier)
			{
				TextMessagingHostingData.CarrierRow carrierRow = (TextMessagingHostingData.CarrierRow)base.NewRow();
				object[] array = new object[2];
				array[0] = Identity;
				object[] array2 = array;
				if (parentCarriersRowByCarriers_Carrier != null)
				{
					array2[1] = parentCarriersRowByCarriers_Carrier[0];
				}
				carrierRow.ItemArray = array2;
				base.Rows.Add(carrierRow);
				return carrierRow;
			}

			// Token: 0x0600040A RID: 1034 RVA: 0x0000FB94 File Offset: 0x0000DD94
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.CarrierRow FindByIdentity(int Identity)
			{
				return (TextMessagingHostingData.CarrierRow)base.Rows.Find(new object[]
				{
					Identity
				});
			}

			// Token: 0x0600040B RID: 1035 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				TextMessagingHostingData.CarrierDataTable carrierDataTable = (TextMessagingHostingData.CarrierDataTable)base.Clone();
				carrierDataTable.InitVars();
				return carrierDataTable;
			}

			// Token: 0x0600040C RID: 1036 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.CarrierDataTable();
			}

			// Token: 0x0600040D RID: 1037 RVA: 0x0000FBEB File Offset: 0x0000DDEB
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnIdentity = base.Columns["Identity"];
				this.columnCarriers_Id = base.Columns["Carriers_Id"];
			}

			// Token: 0x0600040E RID: 1038 RVA: 0x0000FC1C File Offset: 0x0000DE1C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			private void InitClass()
			{
				this.columnIdentity = new DataColumn("Identity", typeof(int), null, MappingType.Attribute);
				base.Columns.Add(this.columnIdentity);
				this.columnCarriers_Id = new DataColumn("Carriers_Id", typeof(int), null, MappingType.Hidden);
				base.Columns.Add(this.columnCarriers_Id);
				base.Constraints.Add(new UniqueConstraint("CarrierKey1", new DataColumn[]
				{
					this.columnIdentity
				}, true));
				this.columnIdentity.AllowDBNull = false;
				this.columnIdentity.Unique = true;
				this.columnIdentity.Namespace = "";
				this.columnCarriers_Id.Namespace = "";
			}

			// Token: 0x0600040F RID: 1039 RVA: 0x0000FCE2 File Offset: 0x0000DEE2
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.CarrierRow NewCarrierRow()
			{
				return (TextMessagingHostingData.CarrierRow)base.NewRow();
			}

			// Token: 0x06000410 RID: 1040 RVA: 0x0000FCEF File Offset: 0x0000DEEF
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.CarrierRow(builder);
			}

			// Token: 0x06000411 RID: 1041 RVA: 0x0000FCF7 File Offset: 0x0000DEF7
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.CarrierRow);
			}

			// Token: 0x06000412 RID: 1042 RVA: 0x0000FD03 File Offset: 0x0000DF03
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.CarrierRowChanged != null)
				{
					this.CarrierRowChanged(this, new TextMessagingHostingData.CarrierRowChangeEvent((TextMessagingHostingData.CarrierRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000413 RID: 1043 RVA: 0x0000FD36 File Offset: 0x0000DF36
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.CarrierRowChanging != null)
				{
					this.CarrierRowChanging(this, new TextMessagingHostingData.CarrierRowChangeEvent((TextMessagingHostingData.CarrierRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000414 RID: 1044 RVA: 0x0000FD69 File Offset: 0x0000DF69
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.CarrierRowDeleted != null)
				{
					this.CarrierRowDeleted(this, new TextMessagingHostingData.CarrierRowChangeEvent((TextMessagingHostingData.CarrierRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000415 RID: 1045 RVA: 0x0000FD9C File Offset: 0x0000DF9C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.CarrierRowDeleting != null)
				{
					this.CarrierRowDeleting(this, new TextMessagingHostingData.CarrierRowChangeEvent((TextMessagingHostingData.CarrierRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000416 RID: 1046 RVA: 0x0000FDCF File Offset: 0x0000DFCF
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveCarrierRow(TextMessagingHostingData.CarrierRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x06000417 RID: 1047 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TextMessagingHostingData textMessagingHostingData = new TextMessagingHostingData();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = textMessagingHostingData.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "CarrierDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = textMessagingHostingData.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}

			// Token: 0x04000133 RID: 307
			private DataColumn columnIdentity;

			// Token: 0x04000134 RID: 308
			private DataColumn columnCarriers_Id;
		}

		// Token: 0x02000054 RID: 84
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class LocalizedInfoDataTable : TypedTableBase<TextMessagingHostingData.LocalizedInfoRow>
		{
			// Token: 0x06000418 RID: 1048 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public LocalizedInfoDataTable()
			{
				base.TableName = "LocalizedInfo";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x06000419 RID: 1049 RVA: 0x00010000 File Offset: 0x0000E200
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal LocalizedInfoDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			// Token: 0x0600041A RID: 1050 RVA: 0x000100A8 File Offset: 0x0000E2A8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected LocalizedInfoDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x0600041B RID: 1051 RVA: 0x000100B8 File Offset: 0x0000E2B8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn DisplayNameColumn
			{
				get
				{
					return this.columnDisplayName;
				}
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x0600041C RID: 1052 RVA: 0x000100C0 File Offset: 0x0000E2C0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CultureColumn
			{
				get
				{
					return this.columnCulture;
				}
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x0600041D RID: 1053 RVA: 0x000100C8 File Offset: 0x0000E2C8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn CarrierIdentityColumn
			{
				get
				{
					return this.columnCarrierIdentity;
				}
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x0600041E RID: 1054 RVA: 0x000100D0 File Offset: 0x0000E2D0
			[Browsable(false)]
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			// Token: 0x17000103 RID: 259
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.LocalizedInfoRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.LocalizedInfoRow)base.Rows[index];
				}
			}

			// Token: 0x14000045 RID: 69
			// (add) Token: 0x06000420 RID: 1056 RVA: 0x000100F0 File Offset: 0x0000E2F0
			// (remove) Token: 0x06000421 RID: 1057 RVA: 0x00010128 File Offset: 0x0000E328
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.LocalizedInfoRowChangeEventHandler LocalizedInfoRowChanging;

			// Token: 0x14000046 RID: 70
			// (add) Token: 0x06000422 RID: 1058 RVA: 0x00010160 File Offset: 0x0000E360
			// (remove) Token: 0x06000423 RID: 1059 RVA: 0x00010198 File Offset: 0x0000E398
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.LocalizedInfoRowChangeEventHandler LocalizedInfoRowChanged;

			// Token: 0x14000047 RID: 71
			// (add) Token: 0x06000424 RID: 1060 RVA: 0x000101D0 File Offset: 0x0000E3D0
			// (remove) Token: 0x06000425 RID: 1061 RVA: 0x00010208 File Offset: 0x0000E408
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.LocalizedInfoRowChangeEventHandler LocalizedInfoRowDeleting;

			// Token: 0x14000048 RID: 72
			// (add) Token: 0x06000426 RID: 1062 RVA: 0x00010240 File Offset: 0x0000E440
			// (remove) Token: 0x06000427 RID: 1063 RVA: 0x00010278 File Offset: 0x0000E478
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.LocalizedInfoRowChangeEventHandler LocalizedInfoRowDeleted;

			// Token: 0x06000428 RID: 1064 RVA: 0x000102AD File Offset: 0x0000E4AD
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void AddLocalizedInfoRow(TextMessagingHostingData.LocalizedInfoRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x000102BC File Offset: 0x0000E4BC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.LocalizedInfoRow AddLocalizedInfoRow(string DisplayName, string Culture, TextMessagingHostingData.CarrierRow parentCarrierRowByCarrier_LocalizedInfo)
			{
				TextMessagingHostingData.LocalizedInfoRow localizedInfoRow = (TextMessagingHostingData.LocalizedInfoRow)base.NewRow();
				object[] array = new object[3];
				array[0] = DisplayName;
				array[1] = Culture;
				object[] array2 = array;
				if (parentCarrierRowByCarrier_LocalizedInfo != null)
				{
					array2[2] = parentCarrierRowByCarrier_LocalizedInfo[0];
				}
				localizedInfoRow.ItemArray = array2;
				base.Rows.Add(localizedInfoRow);
				return localizedInfoRow;
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x00010308 File Offset: 0x0000E508
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.LocalizedInfoRow FindByCultureCarrierIdentity(string Culture, int CarrierIdentity)
			{
				return (TextMessagingHostingData.LocalizedInfoRow)base.Rows.Find(new object[]
				{
					Culture,
					CarrierIdentity
				});
			}

			// Token: 0x0600042B RID: 1067 RVA: 0x0001033C File Offset: 0x0000E53C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				TextMessagingHostingData.LocalizedInfoDataTable localizedInfoDataTable = (TextMessagingHostingData.LocalizedInfoDataTable)base.Clone();
				localizedInfoDataTable.InitVars();
				return localizedInfoDataTable;
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x0001035C File Offset: 0x0000E55C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.LocalizedInfoDataTable();
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x00010364 File Offset: 0x0000E564
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnDisplayName = base.Columns["DisplayName"];
				this.columnCulture = base.Columns["Culture"];
				this.columnCarrierIdentity = base.Columns["CarrierIdentity"];
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x000103B4 File Offset: 0x0000E5B4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			private void InitClass()
			{
				this.columnDisplayName = new DataColumn("DisplayName", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnDisplayName);
				this.columnCulture = new DataColumn("Culture", typeof(string), null, MappingType.Attribute);
				base.Columns.Add(this.columnCulture);
				this.columnCarrierIdentity = new DataColumn("CarrierIdentity", typeof(int), null, MappingType.Hidden);
				base.Columns.Add(this.columnCarrierIdentity);
				base.Constraints.Add(new UniqueConstraint("LocalizedInfoKey1", new DataColumn[]
				{
					this.columnCulture,
					this.columnCarrierIdentity
				}, true));
				this.columnCulture.AllowDBNull = false;
				this.columnCulture.Namespace = "";
				this.columnCarrierIdentity.AllowDBNull = false;
				this.columnCarrierIdentity.Namespace = "";
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x000104B0 File Offset: 0x0000E6B0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.LocalizedInfoRow NewLocalizedInfoRow()
			{
				return (TextMessagingHostingData.LocalizedInfoRow)base.NewRow();
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x000104BD File Offset: 0x0000E6BD
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.LocalizedInfoRow(builder);
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x000104C5 File Offset: 0x0000E6C5
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.LocalizedInfoRow);
			}

			// Token: 0x06000432 RID: 1074 RVA: 0x000104D1 File Offset: 0x0000E6D1
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.LocalizedInfoRowChanged != null)
				{
					this.LocalizedInfoRowChanged(this, new TextMessagingHostingData.LocalizedInfoRowChangeEvent((TextMessagingHostingData.LocalizedInfoRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000433 RID: 1075 RVA: 0x00010504 File Offset: 0x0000E704
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.LocalizedInfoRowChanging != null)
				{
					this.LocalizedInfoRowChanging(this, new TextMessagingHostingData.LocalizedInfoRowChangeEvent((TextMessagingHostingData.LocalizedInfoRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000434 RID: 1076 RVA: 0x00010537 File Offset: 0x0000E737
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.LocalizedInfoRowDeleted != null)
				{
					this.LocalizedInfoRowDeleted(this, new TextMessagingHostingData.LocalizedInfoRowChangeEvent((TextMessagingHostingData.LocalizedInfoRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x0001056A File Offset: 0x0000E76A
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.LocalizedInfoRowDeleting != null)
				{
					this.LocalizedInfoRowDeleting(this, new TextMessagingHostingData.LocalizedInfoRowChangeEvent((TextMessagingHostingData.LocalizedInfoRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000436 RID: 1078 RVA: 0x0001059D File Offset: 0x0000E79D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveLocalizedInfoRow(TextMessagingHostingData.LocalizedInfoRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x06000437 RID: 1079 RVA: 0x000105AC File Offset: 0x0000E7AC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TextMessagingHostingData textMessagingHostingData = new TextMessagingHostingData();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = textMessagingHostingData.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "LocalizedInfoDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = textMessagingHostingData.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}

			// Token: 0x04000139 RID: 313
			private DataColumn columnDisplayName;

			// Token: 0x0400013A RID: 314
			private DataColumn columnCulture;

			// Token: 0x0400013B RID: 315
			private DataColumn columnCarrierIdentity;
		}

		// Token: 0x02000055 RID: 85
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class ServicesDataTable : TypedTableBase<TextMessagingHostingData.ServicesRow>
		{
			// Token: 0x06000438 RID: 1080 RVA: 0x000107A4 File Offset: 0x0000E9A4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public ServicesDataTable()
			{
				base.TableName = "Services";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x06000439 RID: 1081 RVA: 0x000107CC File Offset: 0x0000E9CC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal ServicesDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			// Token: 0x0600043A RID: 1082 RVA: 0x00010874 File Offset: 0x0000EA74
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected ServicesDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x0600043B RID: 1083 RVA: 0x00010884 File Offset: 0x0000EA84
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn Services_IdColumn
			{
				get
				{
					return this.columnServices_Id;
				}
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001088C File Offset: 0x0000EA8C
			[Browsable(false)]
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			// Token: 0x17000106 RID: 262
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.ServicesRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.ServicesRow)base.Rows[index];
				}
			}

			// Token: 0x14000049 RID: 73
			// (add) Token: 0x0600043E RID: 1086 RVA: 0x000108AC File Offset: 0x0000EAAC
			// (remove) Token: 0x0600043F RID: 1087 RVA: 0x000108E4 File Offset: 0x0000EAE4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServicesRowChangeEventHandler ServicesRowChanging;

			// Token: 0x1400004A RID: 74
			// (add) Token: 0x06000440 RID: 1088 RVA: 0x0001091C File Offset: 0x0000EB1C
			// (remove) Token: 0x06000441 RID: 1089 RVA: 0x00010954 File Offset: 0x0000EB54
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServicesRowChangeEventHandler ServicesRowChanged;

			// Token: 0x1400004B RID: 75
			// (add) Token: 0x06000442 RID: 1090 RVA: 0x0001098C File Offset: 0x0000EB8C
			// (remove) Token: 0x06000443 RID: 1091 RVA: 0x000109C4 File Offset: 0x0000EBC4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServicesRowChangeEventHandler ServicesRowDeleting;

			// Token: 0x1400004C RID: 76
			// (add) Token: 0x06000444 RID: 1092 RVA: 0x000109FC File Offset: 0x0000EBFC
			// (remove) Token: 0x06000445 RID: 1093 RVA: 0x00010A34 File Offset: 0x0000EC34
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServicesRowChangeEventHandler ServicesRowDeleted;

			// Token: 0x06000446 RID: 1094 RVA: 0x00010A69 File Offset: 0x0000EC69
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void AddServicesRow(TextMessagingHostingData.ServicesRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x06000447 RID: 1095 RVA: 0x00010A78 File Offset: 0x0000EC78
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.ServicesRow AddServicesRow()
			{
				TextMessagingHostingData.ServicesRow servicesRow = (TextMessagingHostingData.ServicesRow)base.NewRow();
				object[] array = new object[1];
				object[] itemArray = array;
				servicesRow.ItemArray = itemArray;
				base.Rows.Add(servicesRow);
				return servicesRow;
			}

			// Token: 0x06000448 RID: 1096 RVA: 0x00010AB0 File Offset: 0x0000ECB0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				TextMessagingHostingData.ServicesDataTable servicesDataTable = (TextMessagingHostingData.ServicesDataTable)base.Clone();
				servicesDataTable.InitVars();
				return servicesDataTable;
			}

			// Token: 0x06000449 RID: 1097 RVA: 0x00010AD0 File Offset: 0x0000ECD0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.ServicesDataTable();
			}

			// Token: 0x0600044A RID: 1098 RVA: 0x00010AD7 File Offset: 0x0000ECD7
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnServices_Id = base.Columns["Services_Id"];
			}

			// Token: 0x0600044B RID: 1099 RVA: 0x00010AF0 File Offset: 0x0000ECF0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			private void InitClass()
			{
				this.columnServices_Id = new DataColumn("Services_Id", typeof(int), null, MappingType.Hidden);
				base.Columns.Add(this.columnServices_Id);
				base.Constraints.Add(new UniqueConstraint("ServicesKey1", new DataColumn[]
				{
					this.columnServices_Id
				}, true));
				this.columnServices_Id.AutoIncrement = true;
				this.columnServices_Id.AllowDBNull = false;
				this.columnServices_Id.Unique = true;
				this.columnServices_Id.Namespace = "";
			}

			// Token: 0x0600044C RID: 1100 RVA: 0x00010B85 File Offset: 0x0000ED85
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.ServicesRow NewServicesRow()
			{
				return (TextMessagingHostingData.ServicesRow)base.NewRow();
			}

			// Token: 0x0600044D RID: 1101 RVA: 0x00010B92 File Offset: 0x0000ED92
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.ServicesRow(builder);
			}

			// Token: 0x0600044E RID: 1102 RVA: 0x00010B9A File Offset: 0x0000ED9A
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.ServicesRow);
			}

			// Token: 0x0600044F RID: 1103 RVA: 0x00010BA6 File Offset: 0x0000EDA6
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.ServicesRowChanged != null)
				{
					this.ServicesRowChanged(this, new TextMessagingHostingData.ServicesRowChangeEvent((TextMessagingHostingData.ServicesRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000450 RID: 1104 RVA: 0x00010BD9 File Offset: 0x0000EDD9
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.ServicesRowChanging != null)
				{
					this.ServicesRowChanging(this, new TextMessagingHostingData.ServicesRowChangeEvent((TextMessagingHostingData.ServicesRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000451 RID: 1105 RVA: 0x00010C0C File Offset: 0x0000EE0C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.ServicesRowDeleted != null)
				{
					this.ServicesRowDeleted(this, new TextMessagingHostingData.ServicesRowChangeEvent((TextMessagingHostingData.ServicesRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000452 RID: 1106 RVA: 0x00010C3F File Offset: 0x0000EE3F
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.ServicesRowDeleting != null)
				{
					this.ServicesRowDeleting(this, new TextMessagingHostingData.ServicesRowChangeEvent((TextMessagingHostingData.ServicesRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000453 RID: 1107 RVA: 0x00010C72 File Offset: 0x0000EE72
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveServicesRow(TextMessagingHostingData.ServicesRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x06000454 RID: 1108 RVA: 0x00010C80 File Offset: 0x0000EE80
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TextMessagingHostingData textMessagingHostingData = new TextMessagingHostingData();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = textMessagingHostingData.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "ServicesDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = textMessagingHostingData.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}

			// Token: 0x04000140 RID: 320
			private DataColumn columnServices_Id;
		}

		// Token: 0x02000056 RID: 86
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class ServiceDataTable : TypedTableBase<TextMessagingHostingData.ServiceRow>
		{
			// Token: 0x06000455 RID: 1109 RVA: 0x00010E78 File Offset: 0x0000F078
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public ServiceDataTable()
			{
				base.TableName = "Service";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x06000456 RID: 1110 RVA: 0x00010EA0 File Offset: 0x0000F0A0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal ServiceDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			// Token: 0x06000457 RID: 1111 RVA: 0x00010F48 File Offset: 0x0000F148
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected ServiceDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x06000458 RID: 1112 RVA: 0x00010F58 File Offset: 0x0000F158
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn Services_IdColumn
			{
				get
				{
					return this.columnServices_Id;
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000459 RID: 1113 RVA: 0x00010F60 File Offset: 0x0000F160
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn RegionIso2Column
			{
				get
				{
					return this.columnRegionIso2;
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x0600045A RID: 1114 RVA: 0x00010F68 File Offset: 0x0000F168
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CarrierIdentityColumn
			{
				get
				{
					return this.columnCarrierIdentity;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x0600045B RID: 1115 RVA: 0x00010F70 File Offset: 0x0000F170
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn TypeColumn
			{
				get
				{
					return this.columnType;
				}
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x0600045C RID: 1116 RVA: 0x00010F78 File Offset: 0x0000F178
			[Browsable(false)]
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			// Token: 0x1700010C RID: 268
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.ServiceRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.ServiceRow)base.Rows[index];
				}
			}

			// Token: 0x1400004D RID: 77
			// (add) Token: 0x0600045E RID: 1118 RVA: 0x00010F98 File Offset: 0x0000F198
			// (remove) Token: 0x0600045F RID: 1119 RVA: 0x00010FD0 File Offset: 0x0000F1D0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServiceRowChangeEventHandler ServiceRowChanging;

			// Token: 0x1400004E RID: 78
			// (add) Token: 0x06000460 RID: 1120 RVA: 0x00011008 File Offset: 0x0000F208
			// (remove) Token: 0x06000461 RID: 1121 RVA: 0x00011040 File Offset: 0x0000F240
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServiceRowChangeEventHandler ServiceRowChanged;

			// Token: 0x1400004F RID: 79
			// (add) Token: 0x06000462 RID: 1122 RVA: 0x00011078 File Offset: 0x0000F278
			// (remove) Token: 0x06000463 RID: 1123 RVA: 0x000110B0 File Offset: 0x0000F2B0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServiceRowChangeEventHandler ServiceRowDeleting;

			// Token: 0x14000050 RID: 80
			// (add) Token: 0x06000464 RID: 1124 RVA: 0x000110E8 File Offset: 0x0000F2E8
			// (remove) Token: 0x06000465 RID: 1125 RVA: 0x00011120 File Offset: 0x0000F320
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServiceRowChangeEventHandler ServiceRowDeleted;

			// Token: 0x06000466 RID: 1126 RVA: 0x00011155 File Offset: 0x0000F355
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void AddServiceRow(TextMessagingHostingData.ServiceRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x06000467 RID: 1127 RVA: 0x00011164 File Offset: 0x0000F364
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.ServiceRow AddServiceRow(TextMessagingHostingData.ServicesRow parentServicesRowByFK_Services_Service, TextMessagingHostingData.RegionRow parentRegionRowByFK_Region_Service, TextMessagingHostingData.CarrierRow parentCarrierRowByFK_Carrier_Service, string Type)
			{
				TextMessagingHostingData.ServiceRow serviceRow = (TextMessagingHostingData.ServiceRow)base.NewRow();
				object[] array = new object[]
				{
					null,
					null,
					null,
					Type
				};
				if (parentServicesRowByFK_Services_Service != null)
				{
					array[0] = parentServicesRowByFK_Services_Service[0];
				}
				if (parentRegionRowByFK_Region_Service != null)
				{
					array[1] = parentRegionRowByFK_Region_Service[2];
				}
				if (parentCarrierRowByFK_Carrier_Service != null)
				{
					array[2] = parentCarrierRowByFK_Carrier_Service[0];
				}
				serviceRow.ItemArray = array;
				base.Rows.Add(serviceRow);
				return serviceRow;
			}

			// Token: 0x06000468 RID: 1128 RVA: 0x000111C8 File Offset: 0x0000F3C8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.ServiceRow FindByRegionIso2CarrierIdentityType(string RegionIso2, int CarrierIdentity, string Type)
			{
				return (TextMessagingHostingData.ServiceRow)base.Rows.Find(new object[]
				{
					RegionIso2,
					CarrierIdentity,
					Type
				});
			}

			// Token: 0x06000469 RID: 1129 RVA: 0x00011200 File Offset: 0x0000F400
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				TextMessagingHostingData.ServiceDataTable serviceDataTable = (TextMessagingHostingData.ServiceDataTable)base.Clone();
				serviceDataTable.InitVars();
				return serviceDataTable;
			}

			// Token: 0x0600046A RID: 1130 RVA: 0x00011220 File Offset: 0x0000F420
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.ServiceDataTable();
			}

			// Token: 0x0600046B RID: 1131 RVA: 0x00011228 File Offset: 0x0000F428
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnServices_Id = base.Columns["Services_Id"];
				this.columnRegionIso2 = base.Columns["RegionIso2"];
				this.columnCarrierIdentity = base.Columns["CarrierIdentity"];
				this.columnType = base.Columns["Type"];
			}

			// Token: 0x0600046C RID: 1132 RVA: 0x00011290 File Offset: 0x0000F490
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			private void InitClass()
			{
				this.columnServices_Id = new DataColumn("Services_Id", typeof(int), null, MappingType.Hidden);
				base.Columns.Add(this.columnServices_Id);
				this.columnRegionIso2 = new DataColumn("RegionIso2", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnRegionIso2);
				this.columnCarrierIdentity = new DataColumn("CarrierIdentity", typeof(int), null, MappingType.Element);
				base.Columns.Add(this.columnCarrierIdentity);
				this.columnType = new DataColumn("Type", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnType);
				base.Constraints.Add(new UniqueConstraint("ServiceKey1", new DataColumn[]
				{
					this.columnRegionIso2,
					this.columnCarrierIdentity,
					this.columnType
				}, true));
				this.columnServices_Id.Namespace = "";
				this.columnRegionIso2.AllowDBNull = false;
				this.columnCarrierIdentity.AllowDBNull = false;
				this.columnType.AllowDBNull = false;
			}

			// Token: 0x0600046D RID: 1133 RVA: 0x000113BE File Offset: 0x0000F5BE
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.ServiceRow NewServiceRow()
			{
				return (TextMessagingHostingData.ServiceRow)base.NewRow();
			}

			// Token: 0x0600046E RID: 1134 RVA: 0x000113CB File Offset: 0x0000F5CB
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.ServiceRow(builder);
			}

			// Token: 0x0600046F RID: 1135 RVA: 0x000113D3 File Offset: 0x0000F5D3
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.ServiceRow);
			}

			// Token: 0x06000470 RID: 1136 RVA: 0x000113DF File Offset: 0x0000F5DF
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.ServiceRowChanged != null)
				{
					this.ServiceRowChanged(this, new TextMessagingHostingData.ServiceRowChangeEvent((TextMessagingHostingData.ServiceRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000471 RID: 1137 RVA: 0x00011412 File Offset: 0x0000F612
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.ServiceRowChanging != null)
				{
					this.ServiceRowChanging(this, new TextMessagingHostingData.ServiceRowChangeEvent((TextMessagingHostingData.ServiceRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000472 RID: 1138 RVA: 0x00011445 File Offset: 0x0000F645
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.ServiceRowDeleted != null)
				{
					this.ServiceRowDeleted(this, new TextMessagingHostingData.ServiceRowChangeEvent((TextMessagingHostingData.ServiceRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000473 RID: 1139 RVA: 0x00011478 File Offset: 0x0000F678
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.ServiceRowDeleting != null)
				{
					this.ServiceRowDeleting(this, new TextMessagingHostingData.ServiceRowChangeEvent((TextMessagingHostingData.ServiceRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000474 RID: 1140 RVA: 0x000114AB File Offset: 0x0000F6AB
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveServiceRow(TextMessagingHostingData.ServiceRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x06000475 RID: 1141 RVA: 0x000114BC File Offset: 0x0000F6BC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TextMessagingHostingData textMessagingHostingData = new TextMessagingHostingData();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = textMessagingHostingData.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "ServiceDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = textMessagingHostingData.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}

			// Token: 0x04000145 RID: 325
			private DataColumn columnServices_Id;

			// Token: 0x04000146 RID: 326
			private DataColumn columnRegionIso2;

			// Token: 0x04000147 RID: 327
			private DataColumn columnCarrierIdentity;

			// Token: 0x04000148 RID: 328
			private DataColumn columnType;
		}

		// Token: 0x02000057 RID: 87
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class VoiceCallForwardingDataTable : TypedTableBase<TextMessagingHostingData.VoiceCallForwardingRow>
		{
			// Token: 0x06000476 RID: 1142 RVA: 0x000116B4 File Offset: 0x0000F8B4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public VoiceCallForwardingDataTable()
			{
				base.TableName = "VoiceCallForwarding";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x06000477 RID: 1143 RVA: 0x000116DC File Offset: 0x0000F8DC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal VoiceCallForwardingDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			// Token: 0x06000478 RID: 1144 RVA: 0x00011784 File Offset: 0x0000F984
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected VoiceCallForwardingDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x06000479 RID: 1145 RVA: 0x00011794 File Offset: 0x0000F994
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn EnableColumn
			{
				get
				{
					return this.columnEnable;
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x0600047A RID: 1146 RVA: 0x0001179C File Offset: 0x0000F99C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn DisableColumn
			{
				get
				{
					return this.columnDisable;
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x0600047B RID: 1147 RVA: 0x000117A4 File Offset: 0x0000F9A4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn TypeColumn
			{
				get
				{
					return this.columnType;
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x0600047C RID: 1148 RVA: 0x000117AC File Offset: 0x0000F9AC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn RegionIso2Column
			{
				get
				{
					return this.columnRegionIso2;
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x0600047D RID: 1149 RVA: 0x000117B4 File Offset: 0x0000F9B4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CarrierIdentityColumn
			{
				get
				{
					return this.columnCarrierIdentity;
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x0600047E RID: 1150 RVA: 0x000117BC File Offset: 0x0000F9BC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn ServiceTypeColumn
			{
				get
				{
					return this.columnServiceType;
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x0600047F RID: 1151 RVA: 0x000117C4 File Offset: 0x0000F9C4
			[Browsable(false)]
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			// Token: 0x17000114 RID: 276
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.VoiceCallForwardingRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.VoiceCallForwardingRow)base.Rows[index];
				}
			}

			// Token: 0x14000051 RID: 81
			// (add) Token: 0x06000481 RID: 1153 RVA: 0x000117E4 File Offset: 0x0000F9E4
			// (remove) Token: 0x06000482 RID: 1154 RVA: 0x0001181C File Offset: 0x0000FA1C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.VoiceCallForwardingRowChangeEventHandler VoiceCallForwardingRowChanging;

			// Token: 0x14000052 RID: 82
			// (add) Token: 0x06000483 RID: 1155 RVA: 0x00011854 File Offset: 0x0000FA54
			// (remove) Token: 0x06000484 RID: 1156 RVA: 0x0001188C File Offset: 0x0000FA8C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.VoiceCallForwardingRowChangeEventHandler VoiceCallForwardingRowChanged;

			// Token: 0x14000053 RID: 83
			// (add) Token: 0x06000485 RID: 1157 RVA: 0x000118C4 File Offset: 0x0000FAC4
			// (remove) Token: 0x06000486 RID: 1158 RVA: 0x000118FC File Offset: 0x0000FAFC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.VoiceCallForwardingRowChangeEventHandler VoiceCallForwardingRowDeleting;

			// Token: 0x14000054 RID: 84
			// (add) Token: 0x06000487 RID: 1159 RVA: 0x00011934 File Offset: 0x0000FB34
			// (remove) Token: 0x06000488 RID: 1160 RVA: 0x0001196C File Offset: 0x0000FB6C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.VoiceCallForwardingRowChangeEventHandler VoiceCallForwardingRowDeleted;

			// Token: 0x06000489 RID: 1161 RVA: 0x000119A1 File Offset: 0x0000FBA1
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void AddVoiceCallForwardingRow(TextMessagingHostingData.VoiceCallForwardingRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x0600048A RID: 1162 RVA: 0x000119B0 File Offset: 0x0000FBB0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.VoiceCallForwardingRow AddVoiceCallForwardingRow(string Enable, string Disable, string Type, string RegionIso2, int CarrierIdentity, string ServiceType)
			{
				TextMessagingHostingData.VoiceCallForwardingRow voiceCallForwardingRow = (TextMessagingHostingData.VoiceCallForwardingRow)base.NewRow();
				object[] itemArray = new object[]
				{
					Enable,
					Disable,
					Type,
					RegionIso2,
					CarrierIdentity,
					ServiceType
				};
				voiceCallForwardingRow.ItemArray = itemArray;
				base.Rows.Add(voiceCallForwardingRow);
				return voiceCallForwardingRow;
			}

			// Token: 0x0600048B RID: 1163 RVA: 0x00011A08 File Offset: 0x0000FC08
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				TextMessagingHostingData.VoiceCallForwardingDataTable voiceCallForwardingDataTable = (TextMessagingHostingData.VoiceCallForwardingDataTable)base.Clone();
				voiceCallForwardingDataTable.InitVars();
				return voiceCallForwardingDataTable;
			}

			// Token: 0x0600048C RID: 1164 RVA: 0x00011A28 File Offset: 0x0000FC28
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.VoiceCallForwardingDataTable();
			}

			// Token: 0x0600048D RID: 1165 RVA: 0x00011A30 File Offset: 0x0000FC30
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnEnable = base.Columns["Enable"];
				this.columnDisable = base.Columns["Disable"];
				this.columnType = base.Columns["Type"];
				this.columnRegionIso2 = base.Columns["RegionIso2"];
				this.columnCarrierIdentity = base.Columns["CarrierIdentity"];
				this.columnServiceType = base.Columns["ServiceType"];
			}

			// Token: 0x0600048E RID: 1166 RVA: 0x00011AC4 File Offset: 0x0000FCC4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			private void InitClass()
			{
				this.columnEnable = new DataColumn("Enable", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnEnable);
				this.columnDisable = new DataColumn("Disable", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnDisable);
				this.columnType = new DataColumn("Type", typeof(string), null, MappingType.Attribute);
				base.Columns.Add(this.columnType);
				this.columnRegionIso2 = new DataColumn("RegionIso2", typeof(string), null, MappingType.Hidden);
				base.Columns.Add(this.columnRegionIso2);
				this.columnCarrierIdentity = new DataColumn("CarrierIdentity", typeof(int), null, MappingType.Hidden);
				base.Columns.Add(this.columnCarrierIdentity);
				this.columnServiceType = new DataColumn("ServiceType", typeof(string), null, MappingType.Hidden);
				base.Columns.Add(this.columnServiceType);
				base.Constraints.Add(new UniqueConstraint("VoiceCallForwardingKey1", new DataColumn[]
				{
					this.columnRegionIso2,
					this.columnCarrierIdentity,
					this.columnServiceType
				}, true));
				this.columnType.Namespace = "";
				this.columnRegionIso2.AllowDBNull = false;
				this.columnRegionIso2.Namespace = "";
				this.columnCarrierIdentity.AllowDBNull = false;
				this.columnCarrierIdentity.Namespace = "";
				this.columnServiceType.AllowDBNull = false;
				this.columnServiceType.Namespace = "";
			}

			// Token: 0x0600048F RID: 1167 RVA: 0x00011C7C File Offset: 0x0000FE7C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.VoiceCallForwardingRow NewVoiceCallForwardingRow()
			{
				return (TextMessagingHostingData.VoiceCallForwardingRow)base.NewRow();
			}

			// Token: 0x06000490 RID: 1168 RVA: 0x00011C89 File Offset: 0x0000FE89
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.VoiceCallForwardingRow(builder);
			}

			// Token: 0x06000491 RID: 1169 RVA: 0x00011C91 File Offset: 0x0000FE91
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.VoiceCallForwardingRow);
			}

			// Token: 0x06000492 RID: 1170 RVA: 0x00011C9D File Offset: 0x0000FE9D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.VoiceCallForwardingRowChanged != null)
				{
					this.VoiceCallForwardingRowChanged(this, new TextMessagingHostingData.VoiceCallForwardingRowChangeEvent((TextMessagingHostingData.VoiceCallForwardingRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000493 RID: 1171 RVA: 0x00011CD0 File Offset: 0x0000FED0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.VoiceCallForwardingRowChanging != null)
				{
					this.VoiceCallForwardingRowChanging(this, new TextMessagingHostingData.VoiceCallForwardingRowChangeEvent((TextMessagingHostingData.VoiceCallForwardingRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000494 RID: 1172 RVA: 0x00011D03 File Offset: 0x0000FF03
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.VoiceCallForwardingRowDeleted != null)
				{
					this.VoiceCallForwardingRowDeleted(this, new TextMessagingHostingData.VoiceCallForwardingRowChangeEvent((TextMessagingHostingData.VoiceCallForwardingRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000495 RID: 1173 RVA: 0x00011D36 File Offset: 0x0000FF36
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.VoiceCallForwardingRowDeleting != null)
				{
					this.VoiceCallForwardingRowDeleting(this, new TextMessagingHostingData.VoiceCallForwardingRowChangeEvent((TextMessagingHostingData.VoiceCallForwardingRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000496 RID: 1174 RVA: 0x00011D69 File Offset: 0x0000FF69
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveVoiceCallForwardingRow(TextMessagingHostingData.VoiceCallForwardingRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x06000497 RID: 1175 RVA: 0x00011D78 File Offset: 0x0000FF78
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TextMessagingHostingData textMessagingHostingData = new TextMessagingHostingData();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = textMessagingHostingData.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "VoiceCallForwardingDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = textMessagingHostingData.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}

			// Token: 0x0400014D RID: 333
			private DataColumn columnEnable;

			// Token: 0x0400014E RID: 334
			private DataColumn columnDisable;

			// Token: 0x0400014F RID: 335
			private DataColumn columnType;

			// Token: 0x04000150 RID: 336
			private DataColumn columnRegionIso2;

			// Token: 0x04000151 RID: 337
			private DataColumn columnCarrierIdentity;

			// Token: 0x04000152 RID: 338
			private DataColumn columnServiceType;
		}

		// Token: 0x02000058 RID: 88
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class SmtpToSmsGatewayDataTable : TypedTableBase<TextMessagingHostingData.SmtpToSmsGatewayRow>
		{
			// Token: 0x06000498 RID: 1176 RVA: 0x00011F70 File Offset: 0x00010170
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public SmtpToSmsGatewayDataTable()
			{
				base.TableName = "SmtpToSmsGateway";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x06000499 RID: 1177 RVA: 0x00011F98 File Offset: 0x00010198
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal SmtpToSmsGatewayDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			// Token: 0x0600049A RID: 1178 RVA: 0x00012040 File Offset: 0x00010240
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected SmtpToSmsGatewayDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x0600049B RID: 1179 RVA: 0x00012050 File Offset: 0x00010250
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn RegionIso2Column
			{
				get
				{
					return this.columnRegionIso2;
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x0600049C RID: 1180 RVA: 0x00012058 File Offset: 0x00010258
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CarrierIdentityColumn
			{
				get
				{
					return this.columnCarrierIdentity;
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x0600049D RID: 1181 RVA: 0x00012060 File Offset: 0x00010260
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ServiceTypeColumn
			{
				get
				{
					return this.columnServiceType;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x0600049E RID: 1182 RVA: 0x00012068 File Offset: 0x00010268
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[Browsable(false)]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			// Token: 0x17000119 RID: 281
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.SmtpToSmsGatewayRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.SmtpToSmsGatewayRow)base.Rows[index];
				}
			}

			// Token: 0x14000055 RID: 85
			// (add) Token: 0x060004A0 RID: 1184 RVA: 0x00012088 File Offset: 0x00010288
			// (remove) Token: 0x060004A1 RID: 1185 RVA: 0x000120C0 File Offset: 0x000102C0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.SmtpToSmsGatewayRowChangeEventHandler SmtpToSmsGatewayRowChanging;

			// Token: 0x14000056 RID: 86
			// (add) Token: 0x060004A2 RID: 1186 RVA: 0x000120F8 File Offset: 0x000102F8
			// (remove) Token: 0x060004A3 RID: 1187 RVA: 0x00012130 File Offset: 0x00010330
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.SmtpToSmsGatewayRowChangeEventHandler SmtpToSmsGatewayRowChanged;

			// Token: 0x14000057 RID: 87
			// (add) Token: 0x060004A4 RID: 1188 RVA: 0x00012168 File Offset: 0x00010368
			// (remove) Token: 0x060004A5 RID: 1189 RVA: 0x000121A0 File Offset: 0x000103A0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.SmtpToSmsGatewayRowChangeEventHandler SmtpToSmsGatewayRowDeleting;

			// Token: 0x14000058 RID: 88
			// (add) Token: 0x060004A6 RID: 1190 RVA: 0x000121D8 File Offset: 0x000103D8
			// (remove) Token: 0x060004A7 RID: 1191 RVA: 0x00012210 File Offset: 0x00010410
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.SmtpToSmsGatewayRowChangeEventHandler SmtpToSmsGatewayRowDeleted;

			// Token: 0x060004A8 RID: 1192 RVA: 0x00012245 File Offset: 0x00010445
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void AddSmtpToSmsGatewayRow(TextMessagingHostingData.SmtpToSmsGatewayRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x060004A9 RID: 1193 RVA: 0x00012254 File Offset: 0x00010454
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.SmtpToSmsGatewayRow AddSmtpToSmsGatewayRow(string RegionIso2, int CarrierIdentity, string ServiceType)
			{
				TextMessagingHostingData.SmtpToSmsGatewayRow smtpToSmsGatewayRow = (TextMessagingHostingData.SmtpToSmsGatewayRow)base.NewRow();
				object[] itemArray = new object[]
				{
					RegionIso2,
					CarrierIdentity,
					ServiceType
				};
				smtpToSmsGatewayRow.ItemArray = itemArray;
				base.Rows.Add(smtpToSmsGatewayRow);
				return smtpToSmsGatewayRow;
			}

			// Token: 0x060004AA RID: 1194 RVA: 0x0001229C File Offset: 0x0001049C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				TextMessagingHostingData.SmtpToSmsGatewayDataTable smtpToSmsGatewayDataTable = (TextMessagingHostingData.SmtpToSmsGatewayDataTable)base.Clone();
				smtpToSmsGatewayDataTable.InitVars();
				return smtpToSmsGatewayDataTable;
			}

			// Token: 0x060004AB RID: 1195 RVA: 0x000122BC File Offset: 0x000104BC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.SmtpToSmsGatewayDataTable();
			}

			// Token: 0x060004AC RID: 1196 RVA: 0x000122C4 File Offset: 0x000104C4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnRegionIso2 = base.Columns["RegionIso2"];
				this.columnCarrierIdentity = base.Columns["CarrierIdentity"];
				this.columnServiceType = base.Columns["ServiceType"];
			}

			// Token: 0x060004AD RID: 1197 RVA: 0x00012314 File Offset: 0x00010514
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			private void InitClass()
			{
				this.columnRegionIso2 = new DataColumn("RegionIso2", typeof(string), null, MappingType.Hidden);
				base.Columns.Add(this.columnRegionIso2);
				this.columnCarrierIdentity = new DataColumn("CarrierIdentity", typeof(int), null, MappingType.Hidden);
				base.Columns.Add(this.columnCarrierIdentity);
				this.columnServiceType = new DataColumn("ServiceType", typeof(string), null, MappingType.Hidden);
				base.Columns.Add(this.columnServiceType);
				base.Constraints.Add(new UniqueConstraint("SmtpToSmsGatewayKey1", new DataColumn[]
				{
					this.columnRegionIso2,
					this.columnCarrierIdentity,
					this.columnServiceType
				}, true));
				this.columnRegionIso2.AllowDBNull = false;
				this.columnRegionIso2.Namespace = "";
				this.columnCarrierIdentity.AllowDBNull = false;
				this.columnCarrierIdentity.Namespace = "";
				this.columnServiceType.AllowDBNull = false;
				this.columnServiceType.Namespace = "";
			}

			// Token: 0x060004AE RID: 1198 RVA: 0x00012435 File Offset: 0x00010635
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.SmtpToSmsGatewayRow NewSmtpToSmsGatewayRow()
			{
				return (TextMessagingHostingData.SmtpToSmsGatewayRow)base.NewRow();
			}

			// Token: 0x060004AF RID: 1199 RVA: 0x00012442 File Offset: 0x00010642
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.SmtpToSmsGatewayRow(builder);
			}

			// Token: 0x060004B0 RID: 1200 RVA: 0x0001244A File Offset: 0x0001064A
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.SmtpToSmsGatewayRow);
			}

			// Token: 0x060004B1 RID: 1201 RVA: 0x00012456 File Offset: 0x00010656
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.SmtpToSmsGatewayRowChanged != null)
				{
					this.SmtpToSmsGatewayRowChanged(this, new TextMessagingHostingData.SmtpToSmsGatewayRowChangeEvent((TextMessagingHostingData.SmtpToSmsGatewayRow)e.Row, e.Action));
				}
			}

			// Token: 0x060004B2 RID: 1202 RVA: 0x00012489 File Offset: 0x00010689
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.SmtpToSmsGatewayRowChanging != null)
				{
					this.SmtpToSmsGatewayRowChanging(this, new TextMessagingHostingData.SmtpToSmsGatewayRowChangeEvent((TextMessagingHostingData.SmtpToSmsGatewayRow)e.Row, e.Action));
				}
			}

			// Token: 0x060004B3 RID: 1203 RVA: 0x000124BC File Offset: 0x000106BC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.SmtpToSmsGatewayRowDeleted != null)
				{
					this.SmtpToSmsGatewayRowDeleted(this, new TextMessagingHostingData.SmtpToSmsGatewayRowChangeEvent((TextMessagingHostingData.SmtpToSmsGatewayRow)e.Row, e.Action));
				}
			}

			// Token: 0x060004B4 RID: 1204 RVA: 0x000124EF File Offset: 0x000106EF
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.SmtpToSmsGatewayRowDeleting != null)
				{
					this.SmtpToSmsGatewayRowDeleting(this, new TextMessagingHostingData.SmtpToSmsGatewayRowChangeEvent((TextMessagingHostingData.SmtpToSmsGatewayRow)e.Row, e.Action));
				}
			}

			// Token: 0x060004B5 RID: 1205 RVA: 0x00012522 File Offset: 0x00010722
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveSmtpToSmsGatewayRow(TextMessagingHostingData.SmtpToSmsGatewayRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x060004B6 RID: 1206 RVA: 0x00012530 File Offset: 0x00010730
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TextMessagingHostingData textMessagingHostingData = new TextMessagingHostingData();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = textMessagingHostingData.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "SmtpToSmsGatewayDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = textMessagingHostingData.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}

			// Token: 0x04000157 RID: 343
			private DataColumn columnRegionIso2;

			// Token: 0x04000158 RID: 344
			private DataColumn columnCarrierIdentity;

			// Token: 0x04000159 RID: 345
			private DataColumn columnServiceType;
		}

		// Token: 0x02000059 RID: 89
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class RecipientAddressingDataTable : TypedTableBase<TextMessagingHostingData.RecipientAddressingRow>
		{
			// Token: 0x060004B7 RID: 1207 RVA: 0x00012728 File Offset: 0x00010928
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public RecipientAddressingDataTable()
			{
				base.TableName = "RecipientAddressing";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x060004B8 RID: 1208 RVA: 0x00012750 File Offset: 0x00010950
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal RecipientAddressingDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			// Token: 0x060004B9 RID: 1209 RVA: 0x000127F8 File Offset: 0x000109F8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected RecipientAddressingDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x060004BA RID: 1210 RVA: 0x00012808 File Offset: 0x00010A08
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn RegionIso2Column
			{
				get
				{
					return this.columnRegionIso2;
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x060004BB RID: 1211 RVA: 0x00012810 File Offset: 0x00010A10
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn SmtpAddressColumn
			{
				get
				{
					return this.columnSmtpAddress;
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x060004BC RID: 1212 RVA: 0x00012818 File Offset: 0x00010A18
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn CarrierIdentityColumn
			{
				get
				{
					return this.columnCarrierIdentity;
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060004BD RID: 1213 RVA: 0x00012820 File Offset: 0x00010A20
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn ServiceTypeColumn
			{
				get
				{
					return this.columnServiceType;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x060004BE RID: 1214 RVA: 0x00012828 File Offset: 0x00010A28
			[DebuggerNonUserCode]
			[Browsable(false)]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			// Token: 0x1700011F RID: 287
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RecipientAddressingRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.RecipientAddressingRow)base.Rows[index];
				}
			}

			// Token: 0x14000059 RID: 89
			// (add) Token: 0x060004C0 RID: 1216 RVA: 0x00012848 File Offset: 0x00010A48
			// (remove) Token: 0x060004C1 RID: 1217 RVA: 0x00012880 File Offset: 0x00010A80
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RecipientAddressingRowChangeEventHandler RecipientAddressingRowChanging;

			// Token: 0x1400005A RID: 90
			// (add) Token: 0x060004C2 RID: 1218 RVA: 0x000128B8 File Offset: 0x00010AB8
			// (remove) Token: 0x060004C3 RID: 1219 RVA: 0x000128F0 File Offset: 0x00010AF0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RecipientAddressingRowChangeEventHandler RecipientAddressingRowChanged;

			// Token: 0x1400005B RID: 91
			// (add) Token: 0x060004C4 RID: 1220 RVA: 0x00012928 File Offset: 0x00010B28
			// (remove) Token: 0x060004C5 RID: 1221 RVA: 0x00012960 File Offset: 0x00010B60
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RecipientAddressingRowChangeEventHandler RecipientAddressingRowDeleting;

			// Token: 0x1400005C RID: 92
			// (add) Token: 0x060004C6 RID: 1222 RVA: 0x00012998 File Offset: 0x00010B98
			// (remove) Token: 0x060004C7 RID: 1223 RVA: 0x000129D0 File Offset: 0x00010BD0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RecipientAddressingRowChangeEventHandler RecipientAddressingRowDeleted;

			// Token: 0x060004C8 RID: 1224 RVA: 0x00012A05 File Offset: 0x00010C05
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void AddRecipientAddressingRow(TextMessagingHostingData.RecipientAddressingRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x00012A14 File Offset: 0x00010C14
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RecipientAddressingRow AddRecipientAddressingRow(string RegionIso2, string SmtpAddress, int CarrierIdentity, string ServiceType)
			{
				TextMessagingHostingData.RecipientAddressingRow recipientAddressingRow = (TextMessagingHostingData.RecipientAddressingRow)base.NewRow();
				object[] itemArray = new object[]
				{
					RegionIso2,
					SmtpAddress,
					CarrierIdentity,
					ServiceType
				};
				recipientAddressingRow.ItemArray = itemArray;
				base.Rows.Add(recipientAddressingRow);
				return recipientAddressingRow;
			}

			// Token: 0x060004CA RID: 1226 RVA: 0x00012A60 File Offset: 0x00010C60
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				TextMessagingHostingData.RecipientAddressingDataTable recipientAddressingDataTable = (TextMessagingHostingData.RecipientAddressingDataTable)base.Clone();
				recipientAddressingDataTable.InitVars();
				return recipientAddressingDataTable;
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x00012A80 File Offset: 0x00010C80
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.RecipientAddressingDataTable();
			}

			// Token: 0x060004CC RID: 1228 RVA: 0x00012A88 File Offset: 0x00010C88
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnRegionIso2 = base.Columns["RegionIso2"];
				this.columnSmtpAddress = base.Columns["SmtpAddress"];
				this.columnCarrierIdentity = base.Columns["CarrierIdentity"];
				this.columnServiceType = base.Columns["ServiceType"];
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x00012AF0 File Offset: 0x00010CF0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			private void InitClass()
			{
				this.columnRegionIso2 = new DataColumn("RegionIso2", typeof(string), null, MappingType.Hidden);
				base.Columns.Add(this.columnRegionIso2);
				this.columnSmtpAddress = new DataColumn("SmtpAddress", typeof(string), null, MappingType.Element);
				base.Columns.Add(this.columnSmtpAddress);
				this.columnCarrierIdentity = new DataColumn("CarrierIdentity", typeof(int), null, MappingType.Hidden);
				base.Columns.Add(this.columnCarrierIdentity);
				this.columnServiceType = new DataColumn("ServiceType", typeof(string), null, MappingType.Hidden);
				base.Columns.Add(this.columnServiceType);
				base.Constraints.Add(new UniqueConstraint("RecipientAddressingKey1", new DataColumn[]
				{
					this.columnRegionIso2,
					this.columnCarrierIdentity,
					this.columnServiceType
				}, true));
				this.columnRegionIso2.AllowDBNull = false;
				this.columnRegionIso2.Namespace = "";
				this.columnSmtpAddress.MaxLength = 1000;
				this.columnCarrierIdentity.AllowDBNull = false;
				this.columnCarrierIdentity.Namespace = "";
				this.columnServiceType.AllowDBNull = false;
				this.columnServiceType.Namespace = "";
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x00012C4E File Offset: 0x00010E4E
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RecipientAddressingRow NewRecipientAddressingRow()
			{
				return (TextMessagingHostingData.RecipientAddressingRow)base.NewRow();
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x00012C5B File Offset: 0x00010E5B
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.RecipientAddressingRow(builder);
			}

			// Token: 0x060004D0 RID: 1232 RVA: 0x00012C63 File Offset: 0x00010E63
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.RecipientAddressingRow);
			}

			// Token: 0x060004D1 RID: 1233 RVA: 0x00012C6F File Offset: 0x00010E6F
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.RecipientAddressingRowChanged != null)
				{
					this.RecipientAddressingRowChanged(this, new TextMessagingHostingData.RecipientAddressingRowChangeEvent((TextMessagingHostingData.RecipientAddressingRow)e.Row, e.Action));
				}
			}

			// Token: 0x060004D2 RID: 1234 RVA: 0x00012CA2 File Offset: 0x00010EA2
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.RecipientAddressingRowChanging != null)
				{
					this.RecipientAddressingRowChanging(this, new TextMessagingHostingData.RecipientAddressingRowChangeEvent((TextMessagingHostingData.RecipientAddressingRow)e.Row, e.Action));
				}
			}

			// Token: 0x060004D3 RID: 1235 RVA: 0x00012CD5 File Offset: 0x00010ED5
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.RecipientAddressingRowDeleted != null)
				{
					this.RecipientAddressingRowDeleted(this, new TextMessagingHostingData.RecipientAddressingRowChangeEvent((TextMessagingHostingData.RecipientAddressingRow)e.Row, e.Action));
				}
			}

			// Token: 0x060004D4 RID: 1236 RVA: 0x00012D08 File Offset: 0x00010F08
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.RecipientAddressingRowDeleting != null)
				{
					this.RecipientAddressingRowDeleting(this, new TextMessagingHostingData.RecipientAddressingRowChangeEvent((TextMessagingHostingData.RecipientAddressingRow)e.Row, e.Action));
				}
			}

			// Token: 0x060004D5 RID: 1237 RVA: 0x00012D3B File Offset: 0x00010F3B
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void RemoveRecipientAddressingRow(TextMessagingHostingData.RecipientAddressingRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x00012D4C File Offset: 0x00010F4C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TextMessagingHostingData textMessagingHostingData = new TextMessagingHostingData();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = textMessagingHostingData.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "RecipientAddressingDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = textMessagingHostingData.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}

			// Token: 0x0400015E RID: 350
			private DataColumn columnRegionIso2;

			// Token: 0x0400015F RID: 351
			private DataColumn columnSmtpAddress;

			// Token: 0x04000160 RID: 352
			private DataColumn columnCarrierIdentity;

			// Token: 0x04000161 RID: 353
			private DataColumn columnServiceType;
		}

		// Token: 0x0200005A RID: 90
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class MessageRenderingDataTable : TypedTableBase<TextMessagingHostingData.MessageRenderingRow>
		{
			// Token: 0x060004D7 RID: 1239 RVA: 0x00012F44 File Offset: 0x00011144
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public MessageRenderingDataTable()
			{
				base.TableName = "MessageRendering";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x060004D8 RID: 1240 RVA: 0x00012F6C File Offset: 0x0001116C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal MessageRenderingDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			// Token: 0x060004D9 RID: 1241 RVA: 0x00013014 File Offset: 0x00011214
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected MessageRenderingDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060004DA RID: 1242 RVA: 0x00013024 File Offset: 0x00011224
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ContainerColumn
			{
				get
				{
					return this.columnContainer;
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001302C File Offset: 0x0001122C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn RegionIso2Column
			{
				get
				{
					return this.columnRegionIso2;
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x060004DC RID: 1244 RVA: 0x00013034 File Offset: 0x00011234
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn CarrierIdentityColumn
			{
				get
				{
					return this.columnCarrierIdentity;
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x060004DD RID: 1245 RVA: 0x0001303C File Offset: 0x0001123C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ServiceTypeColumn
			{
				get
				{
					return this.columnServiceType;
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x060004DE RID: 1246 RVA: 0x00013044 File Offset: 0x00011244
			[DebuggerNonUserCode]
			[Browsable(false)]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			// Token: 0x17000125 RID: 293
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.MessageRenderingRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.MessageRenderingRow)base.Rows[index];
				}
			}

			// Token: 0x1400005D RID: 93
			// (add) Token: 0x060004E0 RID: 1248 RVA: 0x00013064 File Offset: 0x00011264
			// (remove) Token: 0x060004E1 RID: 1249 RVA: 0x0001309C File Offset: 0x0001129C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.MessageRenderingRowChangeEventHandler MessageRenderingRowChanging;

			// Token: 0x1400005E RID: 94
			// (add) Token: 0x060004E2 RID: 1250 RVA: 0x000130D4 File Offset: 0x000112D4
			// (remove) Token: 0x060004E3 RID: 1251 RVA: 0x0001310C File Offset: 0x0001130C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.MessageRenderingRowChangeEventHandler MessageRenderingRowChanged;

			// Token: 0x1400005F RID: 95
			// (add) Token: 0x060004E4 RID: 1252 RVA: 0x00013144 File Offset: 0x00011344
			// (remove) Token: 0x060004E5 RID: 1253 RVA: 0x0001317C File Offset: 0x0001137C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.MessageRenderingRowChangeEventHandler MessageRenderingRowDeleting;

			// Token: 0x14000060 RID: 96
			// (add) Token: 0x060004E6 RID: 1254 RVA: 0x000131B4 File Offset: 0x000113B4
			// (remove) Token: 0x060004E7 RID: 1255 RVA: 0x000131EC File Offset: 0x000113EC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.MessageRenderingRowChangeEventHandler MessageRenderingRowDeleted;

			// Token: 0x060004E8 RID: 1256 RVA: 0x00013221 File Offset: 0x00011421
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void AddMessageRenderingRow(TextMessagingHostingData.MessageRenderingRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x00013230 File Offset: 0x00011430
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.MessageRenderingRow AddMessageRenderingRow(string Container, string RegionIso2, int CarrierIdentity, string ServiceType)
			{
				TextMessagingHostingData.MessageRenderingRow messageRenderingRow = (TextMessagingHostingData.MessageRenderingRow)base.NewRow();
				object[] itemArray = new object[]
				{
					Container,
					RegionIso2,
					CarrierIdentity,
					ServiceType
				};
				messageRenderingRow.ItemArray = itemArray;
				base.Rows.Add(messageRenderingRow);
				return messageRenderingRow;
			}

			// Token: 0x060004EA RID: 1258 RVA: 0x0001327C File Offset: 0x0001147C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				TextMessagingHostingData.MessageRenderingDataTable messageRenderingDataTable = (TextMessagingHostingData.MessageRenderingDataTable)base.Clone();
				messageRenderingDataTable.InitVars();
				return messageRenderingDataTable;
			}

			// Token: 0x060004EB RID: 1259 RVA: 0x0001329C File Offset: 0x0001149C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.MessageRenderingDataTable();
			}

			// Token: 0x060004EC RID: 1260 RVA: 0x000132A4 File Offset: 0x000114A4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal void InitVars()
			{
				this.columnContainer = base.Columns["Container"];
				this.columnRegionIso2 = base.Columns["RegionIso2"];
				this.columnCarrierIdentity = base.Columns["CarrierIdentity"];
				this.columnServiceType = base.Columns["ServiceType"];
			}

			// Token: 0x060004ED RID: 1261 RVA: 0x0001330C File Offset: 0x0001150C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			private void InitClass()
			{
				this.columnContainer = new DataColumn("Container", typeof(string), null, MappingType.Attribute);
				base.Columns.Add(this.columnContainer);
				this.columnRegionIso2 = new DataColumn("RegionIso2", typeof(string), null, MappingType.Hidden);
				base.Columns.Add(this.columnRegionIso2);
				this.columnCarrierIdentity = new DataColumn("CarrierIdentity", typeof(int), null, MappingType.Hidden);
				base.Columns.Add(this.columnCarrierIdentity);
				this.columnServiceType = new DataColumn("ServiceType", typeof(string), null, MappingType.Hidden);
				base.Columns.Add(this.columnServiceType);
				base.Constraints.Add(new UniqueConstraint("MessageRenderingKey1", new DataColumn[]
				{
					this.columnRegionIso2,
					this.columnCarrierIdentity,
					this.columnServiceType
				}, true));
				this.columnContainer.Namespace = "";
				this.columnRegionIso2.AllowDBNull = false;
				this.columnRegionIso2.Namespace = "";
				this.columnCarrierIdentity.AllowDBNull = false;
				this.columnCarrierIdentity.Namespace = "";
				this.columnServiceType.AllowDBNull = false;
				this.columnServiceType.Namespace = "";
			}

			// Token: 0x060004EE RID: 1262 RVA: 0x0001346A File Offset: 0x0001166A
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.MessageRenderingRow NewMessageRenderingRow()
			{
				return (TextMessagingHostingData.MessageRenderingRow)base.NewRow();
			}

			// Token: 0x060004EF RID: 1263 RVA: 0x00013477 File Offset: 0x00011677
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.MessageRenderingRow(builder);
			}

			// Token: 0x060004F0 RID: 1264 RVA: 0x0001347F File Offset: 0x0001167F
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.MessageRenderingRow);
			}

			// Token: 0x060004F1 RID: 1265 RVA: 0x0001348B File Offset: 0x0001168B
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.MessageRenderingRowChanged != null)
				{
					this.MessageRenderingRowChanged(this, new TextMessagingHostingData.MessageRenderingRowChangeEvent((TextMessagingHostingData.MessageRenderingRow)e.Row, e.Action));
				}
			}

			// Token: 0x060004F2 RID: 1266 RVA: 0x000134BE File Offset: 0x000116BE
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.MessageRenderingRowChanging != null)
				{
					this.MessageRenderingRowChanging(this, new TextMessagingHostingData.MessageRenderingRowChangeEvent((TextMessagingHostingData.MessageRenderingRow)e.Row, e.Action));
				}
			}

			// Token: 0x060004F3 RID: 1267 RVA: 0x000134F1 File Offset: 0x000116F1
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.MessageRenderingRowDeleted != null)
				{
					this.MessageRenderingRowDeleted(this, new TextMessagingHostingData.MessageRenderingRowChangeEvent((TextMessagingHostingData.MessageRenderingRow)e.Row, e.Action));
				}
			}

			// Token: 0x060004F4 RID: 1268 RVA: 0x00013524 File Offset: 0x00011724
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.MessageRenderingRowDeleting != null)
				{
					this.MessageRenderingRowDeleting(this, new TextMessagingHostingData.MessageRenderingRowChangeEvent((TextMessagingHostingData.MessageRenderingRow)e.Row, e.Action));
				}
			}

			// Token: 0x060004F5 RID: 1269 RVA: 0x00013557 File Offset: 0x00011757
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void RemoveMessageRenderingRow(TextMessagingHostingData.MessageRenderingRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x060004F6 RID: 1270 RVA: 0x00013568 File Offset: 0x00011768
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TextMessagingHostingData textMessagingHostingData = new TextMessagingHostingData();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = textMessagingHostingData.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "MessageRenderingDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = textMessagingHostingData.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}

			// Token: 0x04000166 RID: 358
			private DataColumn columnContainer;

			// Token: 0x04000167 RID: 359
			private DataColumn columnRegionIso2;

			// Token: 0x04000168 RID: 360
			private DataColumn columnCarrierIdentity;

			// Token: 0x04000169 RID: 361
			private DataColumn columnServiceType;
		}

		// Token: 0x0200005B RID: 91
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class CapacityDataTable : TypedTableBase<TextMessagingHostingData.CapacityRow>
		{
			// Token: 0x060004F7 RID: 1271 RVA: 0x00013760 File Offset: 0x00011960
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public CapacityDataTable()
			{
				base.TableName = "Capacity";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x060004F8 RID: 1272 RVA: 0x00013788 File Offset: 0x00011988
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal CapacityDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			// Token: 0x060004F9 RID: 1273 RVA: 0x00013830 File Offset: 0x00011A30
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected CapacityDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x060004FA RID: 1274 RVA: 0x00013840 File Offset: 0x00011A40
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CodingSchemeColumn
			{
				get
				{
					return this.columnCodingScheme;
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x060004FB RID: 1275 RVA: 0x00013848 File Offset: 0x00011A48
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn Capacity_ValueColumn
			{
				get
				{
					return this.columnCapacity_Value;
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x060004FC RID: 1276 RVA: 0x00013850 File Offset: 0x00011A50
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn RegionIso2Column
			{
				get
				{
					return this.columnRegionIso2;
				}
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x060004FD RID: 1277 RVA: 0x00013858 File Offset: 0x00011A58
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CarrierIdentityColumn
			{
				get
				{
					return this.columnCarrierIdentity;
				}
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x060004FE RID: 1278 RVA: 0x00013860 File Offset: 0x00011A60
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn ServiceTypeColumn
			{
				get
				{
					return this.columnServiceType;
				}
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x060004FF RID: 1279 RVA: 0x00013868 File Offset: 0x00011A68
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[Browsable(false)]
			[DebuggerNonUserCode]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			// Token: 0x1700012C RID: 300
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CapacityRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.CapacityRow)base.Rows[index];
				}
			}

			// Token: 0x14000061 RID: 97
			// (add) Token: 0x06000501 RID: 1281 RVA: 0x00013888 File Offset: 0x00011A88
			// (remove) Token: 0x06000502 RID: 1282 RVA: 0x000138C0 File Offset: 0x00011AC0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CapacityRowChangeEventHandler CapacityRowChanging;

			// Token: 0x14000062 RID: 98
			// (add) Token: 0x06000503 RID: 1283 RVA: 0x000138F8 File Offset: 0x00011AF8
			// (remove) Token: 0x06000504 RID: 1284 RVA: 0x00013930 File Offset: 0x00011B30
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CapacityRowChangeEventHandler CapacityRowChanged;

			// Token: 0x14000063 RID: 99
			// (add) Token: 0x06000505 RID: 1285 RVA: 0x00013968 File Offset: 0x00011B68
			// (remove) Token: 0x06000506 RID: 1286 RVA: 0x000139A0 File Offset: 0x00011BA0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CapacityRowChangeEventHandler CapacityRowDeleting;

			// Token: 0x14000064 RID: 100
			// (add) Token: 0x06000507 RID: 1287 RVA: 0x000139D8 File Offset: 0x00011BD8
			// (remove) Token: 0x06000508 RID: 1288 RVA: 0x00013A10 File Offset: 0x00011C10
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CapacityRowChangeEventHandler CapacityRowDeleted;

			// Token: 0x06000509 RID: 1289 RVA: 0x00013A45 File Offset: 0x00011C45
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void AddCapacityRow(TextMessagingHostingData.CapacityRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x0600050A RID: 1290 RVA: 0x00013A54 File Offset: 0x00011C54
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CapacityRow AddCapacityRow(string CodingScheme, int Capacity_Value, string RegionIso2, int CarrierIdentity, string ServiceType)
			{
				TextMessagingHostingData.CapacityRow capacityRow = (TextMessagingHostingData.CapacityRow)base.NewRow();
				object[] itemArray = new object[]
				{
					CodingScheme,
					Capacity_Value,
					RegionIso2,
					CarrierIdentity,
					ServiceType
				};
				capacityRow.ItemArray = itemArray;
				base.Rows.Add(capacityRow);
				return capacityRow;
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x00013AAC File Offset: 0x00011CAC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CapacityRow FindByRegionIso2CarrierIdentityServiceTypeCodingScheme(string RegionIso2, int CarrierIdentity, string ServiceType, string CodingScheme)
			{
				return (TextMessagingHostingData.CapacityRow)base.Rows.Find(new object[]
				{
					RegionIso2,
					CarrierIdentity,
					ServiceType,
					CodingScheme
				});
			}

			// Token: 0x0600050C RID: 1292 RVA: 0x00013AE8 File Offset: 0x00011CE8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				TextMessagingHostingData.CapacityDataTable capacityDataTable = (TextMessagingHostingData.CapacityDataTable)base.Clone();
				capacityDataTable.InitVars();
				return capacityDataTable;
			}

			// Token: 0x0600050D RID: 1293 RVA: 0x00013B08 File Offset: 0x00011D08
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.CapacityDataTable();
			}

			// Token: 0x0600050E RID: 1294 RVA: 0x00013B10 File Offset: 0x00011D10
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnCodingScheme = base.Columns["CodingScheme"];
				this.columnCapacity_Value = base.Columns["Capacity_Value"];
				this.columnRegionIso2 = base.Columns["RegionIso2"];
				this.columnCarrierIdentity = base.Columns["CarrierIdentity"];
				this.columnServiceType = base.Columns["ServiceType"];
			}

			// Token: 0x0600050F RID: 1295 RVA: 0x00013B8C File Offset: 0x00011D8C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			private void InitClass()
			{
				this.columnCodingScheme = new DataColumn("CodingScheme", typeof(string), null, MappingType.Attribute);
				base.Columns.Add(this.columnCodingScheme);
				this.columnCapacity_Value = new DataColumn("Capacity_Value", typeof(int), null, MappingType.SimpleContent);
				base.Columns.Add(this.columnCapacity_Value);
				this.columnRegionIso2 = new DataColumn("RegionIso2", typeof(string), null, MappingType.Hidden);
				base.Columns.Add(this.columnRegionIso2);
				this.columnCarrierIdentity = new DataColumn("CarrierIdentity", typeof(int), null, MappingType.Hidden);
				base.Columns.Add(this.columnCarrierIdentity);
				this.columnServiceType = new DataColumn("ServiceType", typeof(string), null, MappingType.Hidden);
				base.Columns.Add(this.columnServiceType);
				base.Constraints.Add(new UniqueConstraint("CapacityKey1", new DataColumn[]
				{
					this.columnRegionIso2,
					this.columnCarrierIdentity,
					this.columnServiceType,
					this.columnCodingScheme
				}, true));
				this.columnCodingScheme.AllowDBNull = false;
				this.columnCodingScheme.Namespace = "";
				this.columnRegionIso2.AllowDBNull = false;
				this.columnRegionIso2.Namespace = "";
				this.columnCarrierIdentity.AllowDBNull = false;
				this.columnCarrierIdentity.Namespace = "";
				this.columnServiceType.AllowDBNull = false;
				this.columnServiceType.Namespace = "";
			}

			// Token: 0x06000510 RID: 1296 RVA: 0x00013D2C File Offset: 0x00011F2C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.CapacityRow NewCapacityRow()
			{
				return (TextMessagingHostingData.CapacityRow)base.NewRow();
			}

			// Token: 0x06000511 RID: 1297 RVA: 0x00013D39 File Offset: 0x00011F39
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.CapacityRow(builder);
			}

			// Token: 0x06000512 RID: 1298 RVA: 0x00013D41 File Offset: 0x00011F41
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.CapacityRow);
			}

			// Token: 0x06000513 RID: 1299 RVA: 0x00013D4D File Offset: 0x00011F4D
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.CapacityRowChanged != null)
				{
					this.CapacityRowChanged(this, new TextMessagingHostingData.CapacityRowChangeEvent((TextMessagingHostingData.CapacityRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000514 RID: 1300 RVA: 0x00013D80 File Offset: 0x00011F80
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.CapacityRowChanging != null)
				{
					this.CapacityRowChanging(this, new TextMessagingHostingData.CapacityRowChangeEvent((TextMessagingHostingData.CapacityRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000515 RID: 1301 RVA: 0x00013DB3 File Offset: 0x00011FB3
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.CapacityRowDeleted != null)
				{
					this.CapacityRowDeleted(this, new TextMessagingHostingData.CapacityRowChangeEvent((TextMessagingHostingData.CapacityRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000516 RID: 1302 RVA: 0x00013DE6 File Offset: 0x00011FE6
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.CapacityRowDeleting != null)
				{
					this.CapacityRowDeleting(this, new TextMessagingHostingData.CapacityRowChangeEvent((TextMessagingHostingData.CapacityRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000517 RID: 1303 RVA: 0x00013E19 File Offset: 0x00012019
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void RemoveCapacityRow(TextMessagingHostingData.CapacityRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x06000518 RID: 1304 RVA: 0x00013E28 File Offset: 0x00012028
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				TextMessagingHostingData textMessagingHostingData = new TextMessagingHostingData();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = textMessagingHostingData.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "CapacityDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = textMessagingHostingData.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						schemaSerializable.Write(memoryStream);
						foreach (object obj in xs.Schemas(schemaSerializable.TargetNamespace))
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (memoryStream2 != null)
						{
							memoryStream2.Close();
						}
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}

			// Token: 0x0400016E RID: 366
			private DataColumn columnCodingScheme;

			// Token: 0x0400016F RID: 367
			private DataColumn columnCapacity_Value;

			// Token: 0x04000170 RID: 368
			private DataColumn columnRegionIso2;

			// Token: 0x04000171 RID: 369
			private DataColumn columnCarrierIdentity;

			// Token: 0x04000172 RID: 370
			private DataColumn columnServiceType;
		}

		// Token: 0x0200005C RID: 92
		public class _locDefinitionRow : DataRow
		{
			// Token: 0x06000519 RID: 1305 RVA: 0x00014020 File Offset: 0x00012220
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal _locDefinitionRow(DataRowBuilder rb) : base(rb)
			{
				this.table_locDefinition = (TextMessagingHostingData._locDefinitionDataTable)base.Table;
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x0600051A RID: 1306 RVA: 0x0001403A File Offset: 0x0001223A
			// (set) Token: 0x0600051B RID: 1307 RVA: 0x00014052 File Offset: 0x00012252
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public int _locDefinition_Id
			{
				get
				{
					return (int)base[this.table_locDefinition._locDefinition_IdColumn];
				}
				set
				{
					base[this.table_locDefinition._locDefinition_IdColumn] = value;
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001406B File Offset: 0x0001226B
			// (set) Token: 0x0600051D RID: 1309 RVA: 0x00014083 File Offset: 0x00012283
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public string _locDefault_loc
			{
				get
				{
					return (string)base[this.table_locDefinition._locDefault_locColumn];
				}
				set
				{
					base[this.table_locDefinition._locDefault_locColumn] = value;
				}
			}

			// Token: 0x04000177 RID: 375
			private TextMessagingHostingData._locDefinitionDataTable table_locDefinition;
		}

		// Token: 0x0200005D RID: 93
		public class RegionsRow : DataRow
		{
			// Token: 0x0600051E RID: 1310 RVA: 0x00014097 File Offset: 0x00012297
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal RegionsRow(DataRowBuilder rb) : base(rb)
			{
				this.tableRegions = (TextMessagingHostingData.RegionsDataTable)base.Table;
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x0600051F RID: 1311 RVA: 0x000140B1 File Offset: 0x000122B1
			// (set) Token: 0x06000520 RID: 1312 RVA: 0x000140C9 File Offset: 0x000122C9
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public int Regions_Id
			{
				get
				{
					return (int)base[this.tableRegions.Regions_IdColumn];
				}
				set
				{
					base[this.tableRegions.Regions_IdColumn] = value;
				}
			}

			// Token: 0x06000521 RID: 1313 RVA: 0x000140E2 File Offset: 0x000122E2
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RegionRow[] GetRegionRows()
			{
				if (base.Table.ChildRelations["Regions_Region"] == null)
				{
					return new TextMessagingHostingData.RegionRow[0];
				}
				return (TextMessagingHostingData.RegionRow[])base.GetChildRows(base.Table.ChildRelations["Regions_Region"]);
			}

			// Token: 0x04000178 RID: 376
			private TextMessagingHostingData.RegionsDataTable tableRegions;
		}

		// Token: 0x0200005E RID: 94
		public class RegionRow : DataRow
		{
			// Token: 0x06000522 RID: 1314 RVA: 0x00014122 File Offset: 0x00012322
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal RegionRow(DataRowBuilder rb) : base(rb)
			{
				this.tableRegion = (TextMessagingHostingData.RegionDataTable)base.Table;
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001413C File Offset: 0x0001233C
			// (set) Token: 0x06000524 RID: 1316 RVA: 0x00014154 File Offset: 0x00012354
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string CountryCode
			{
				get
				{
					return (string)base[this.tableRegion.CountryCodeColumn];
				}
				set
				{
					base[this.tableRegion.CountryCodeColumn] = value;
				}
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x06000525 RID: 1317 RVA: 0x00014168 File Offset: 0x00012368
			// (set) Token: 0x06000526 RID: 1318 RVA: 0x000141AC File Offset: 0x000123AC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string PhoneNumberExample
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableRegion.PhoneNumberExampleColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'PhoneNumberExample' in table 'Region' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableRegion.PhoneNumberExampleColumn] = value;
				}
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x06000527 RID: 1319 RVA: 0x000141C0 File Offset: 0x000123C0
			// (set) Token: 0x06000528 RID: 1320 RVA: 0x000141D8 File Offset: 0x000123D8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string Iso2
			{
				get
				{
					return (string)base[this.tableRegion.Iso2Column];
				}
				set
				{
					base[this.tableRegion.Iso2Column] = value;
				}
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x06000529 RID: 1321 RVA: 0x000141EC File Offset: 0x000123EC
			// (set) Token: 0x0600052A RID: 1322 RVA: 0x00014230 File Offset: 0x00012430
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int Regions_Id
			{
				get
				{
					int result;
					try
					{
						result = (int)base[this.tableRegion.Regions_IdColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Regions_Id' in table 'Region' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableRegion.Regions_IdColumn] = value;
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x0600052B RID: 1323 RVA: 0x00014249 File Offset: 0x00012449
			// (set) Token: 0x0600052C RID: 1324 RVA: 0x0001426B File Offset: 0x0001246B
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RegionsRow RegionsRow
			{
				get
				{
					return (TextMessagingHostingData.RegionsRow)base.GetParentRow(base.Table.ParentRelations["Regions_Region"]);
				}
				set
				{
					base.SetParentRow(value, base.Table.ParentRelations["Regions_Region"]);
				}
			}

			// Token: 0x0600052D RID: 1325 RVA: 0x00014289 File Offset: 0x00012489
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsPhoneNumberExampleNull()
			{
				return base.IsNull(this.tableRegion.PhoneNumberExampleColumn);
			}

			// Token: 0x0600052E RID: 1326 RVA: 0x0001429C File Offset: 0x0001249C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void SetPhoneNumberExampleNull()
			{
				base[this.tableRegion.PhoneNumberExampleColumn] = Convert.DBNull;
			}

			// Token: 0x0600052F RID: 1327 RVA: 0x000142B4 File Offset: 0x000124B4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsRegions_IdNull()
			{
				return base.IsNull(this.tableRegion.Regions_IdColumn);
			}

			// Token: 0x06000530 RID: 1328 RVA: 0x000142C7 File Offset: 0x000124C7
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetRegions_IdNull()
			{
				base[this.tableRegion.Regions_IdColumn] = Convert.DBNull;
			}

			// Token: 0x06000531 RID: 1329 RVA: 0x000142DF File Offset: 0x000124DF
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.ServiceRow[] GetServiceRows()
			{
				if (base.Table.ChildRelations["FK_Region_Service"] == null)
				{
					return new TextMessagingHostingData.ServiceRow[0];
				}
				return (TextMessagingHostingData.ServiceRow[])base.GetChildRows(base.Table.ChildRelations["FK_Region_Service"]);
			}

			// Token: 0x04000179 RID: 377
			private TextMessagingHostingData.RegionDataTable tableRegion;
		}

		// Token: 0x0200005F RID: 95
		public class CarriersRow : DataRow
		{
			// Token: 0x06000532 RID: 1330 RVA: 0x0001431F File Offset: 0x0001251F
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal CarriersRow(DataRowBuilder rb) : base(rb)
			{
				this.tableCarriers = (TextMessagingHostingData.CarriersDataTable)base.Table;
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x06000533 RID: 1331 RVA: 0x00014339 File Offset: 0x00012539
			// (set) Token: 0x06000534 RID: 1332 RVA: 0x00014351 File Offset: 0x00012551
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int Carriers_Id
			{
				get
				{
					return (int)base[this.tableCarriers.Carriers_IdColumn];
				}
				set
				{
					base[this.tableCarriers.Carriers_IdColumn] = value;
				}
			}

			// Token: 0x06000535 RID: 1333 RVA: 0x0001436A File Offset: 0x0001256A
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CarrierRow[] GetCarrierRows()
			{
				if (base.Table.ChildRelations["Carriers_Carrier"] == null)
				{
					return new TextMessagingHostingData.CarrierRow[0];
				}
				return (TextMessagingHostingData.CarrierRow[])base.GetChildRows(base.Table.ChildRelations["Carriers_Carrier"]);
			}

			// Token: 0x0400017A RID: 378
			private TextMessagingHostingData.CarriersDataTable tableCarriers;
		}

		// Token: 0x02000060 RID: 96
		public class CarrierRow : DataRow
		{
			// Token: 0x06000536 RID: 1334 RVA: 0x000143AA File Offset: 0x000125AA
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal CarrierRow(DataRowBuilder rb) : base(rb)
			{
				this.tableCarrier = (TextMessagingHostingData.CarrierDataTable)base.Table;
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x06000537 RID: 1335 RVA: 0x000143C4 File Offset: 0x000125C4
			// (set) Token: 0x06000538 RID: 1336 RVA: 0x000143DC File Offset: 0x000125DC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public int Identity
			{
				get
				{
					return (int)base[this.tableCarrier.IdentityColumn];
				}
				set
				{
					base[this.tableCarrier.IdentityColumn] = value;
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x06000539 RID: 1337 RVA: 0x000143F8 File Offset: 0x000125F8
			// (set) Token: 0x0600053A RID: 1338 RVA: 0x0001443C File Offset: 0x0001263C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public int Carriers_Id
			{
				get
				{
					int result;
					try
					{
						result = (int)base[this.tableCarrier.Carriers_IdColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Carriers_Id' in table 'Carrier' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableCarrier.Carriers_IdColumn] = value;
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x0600053B RID: 1339 RVA: 0x00014455 File Offset: 0x00012655
			// (set) Token: 0x0600053C RID: 1340 RVA: 0x00014477 File Offset: 0x00012677
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CarriersRow CarriersRow
			{
				get
				{
					return (TextMessagingHostingData.CarriersRow)base.GetParentRow(base.Table.ParentRelations["Carriers_Carrier"]);
				}
				set
				{
					base.SetParentRow(value, base.Table.ParentRelations["Carriers_Carrier"]);
				}
			}

			// Token: 0x0600053D RID: 1341 RVA: 0x00014495 File Offset: 0x00012695
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsCarriers_IdNull()
			{
				return base.IsNull(this.tableCarrier.Carriers_IdColumn);
			}

			// Token: 0x0600053E RID: 1342 RVA: 0x000144A8 File Offset: 0x000126A8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetCarriers_IdNull()
			{
				base[this.tableCarrier.Carriers_IdColumn] = Convert.DBNull;
			}

			// Token: 0x0600053F RID: 1343 RVA: 0x000144C0 File Offset: 0x000126C0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.LocalizedInfoRow[] GetLocalizedInfoRows()
			{
				if (base.Table.ChildRelations["Carrier_LocalizedInfo"] == null)
				{
					return new TextMessagingHostingData.LocalizedInfoRow[0];
				}
				return (TextMessagingHostingData.LocalizedInfoRow[])base.GetChildRows(base.Table.ChildRelations["Carrier_LocalizedInfo"]);
			}

			// Token: 0x06000540 RID: 1344 RVA: 0x00014500 File Offset: 0x00012700
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.ServiceRow[] GetServiceRows()
			{
				if (base.Table.ChildRelations["FK_Carrier_Service"] == null)
				{
					return new TextMessagingHostingData.ServiceRow[0];
				}
				return (TextMessagingHostingData.ServiceRow[])base.GetChildRows(base.Table.ChildRelations["FK_Carrier_Service"]);
			}

			// Token: 0x0400017B RID: 379
			private TextMessagingHostingData.CarrierDataTable tableCarrier;
		}

		// Token: 0x02000061 RID: 97
		public class LocalizedInfoRow : DataRow
		{
			// Token: 0x06000541 RID: 1345 RVA: 0x00014540 File Offset: 0x00012740
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal LocalizedInfoRow(DataRowBuilder rb) : base(rb)
			{
				this.tableLocalizedInfo = (TextMessagingHostingData.LocalizedInfoDataTable)base.Table;
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001455C File Offset: 0x0001275C
			// (set) Token: 0x06000543 RID: 1347 RVA: 0x000145A0 File Offset: 0x000127A0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public string DisplayName
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableLocalizedInfo.DisplayNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'DisplayName' in table 'LocalizedInfo' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableLocalizedInfo.DisplayNameColumn] = value;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x06000544 RID: 1348 RVA: 0x000145B4 File Offset: 0x000127B4
			// (set) Token: 0x06000545 RID: 1349 RVA: 0x000145CC File Offset: 0x000127CC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string Culture
			{
				get
				{
					return (string)base[this.tableLocalizedInfo.CultureColumn];
				}
				set
				{
					base[this.tableLocalizedInfo.CultureColumn] = value;
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x06000546 RID: 1350 RVA: 0x000145E0 File Offset: 0x000127E0
			// (set) Token: 0x06000547 RID: 1351 RVA: 0x000145F8 File Offset: 0x000127F8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int CarrierIdentity
			{
				get
				{
					return (int)base[this.tableLocalizedInfo.CarrierIdentityColumn];
				}
				set
				{
					base[this.tableLocalizedInfo.CarrierIdentityColumn] = value;
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x06000548 RID: 1352 RVA: 0x00014611 File Offset: 0x00012811
			// (set) Token: 0x06000549 RID: 1353 RVA: 0x00014633 File Offset: 0x00012833
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CarrierRow CarrierRow
			{
				get
				{
					return (TextMessagingHostingData.CarrierRow)base.GetParentRow(base.Table.ParentRelations["Carrier_LocalizedInfo"]);
				}
				set
				{
					base.SetParentRow(value, base.Table.ParentRelations["Carrier_LocalizedInfo"]);
				}
			}

			// Token: 0x0600054A RID: 1354 RVA: 0x00014651 File Offset: 0x00012851
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsDisplayNameNull()
			{
				return base.IsNull(this.tableLocalizedInfo.DisplayNameColumn);
			}

			// Token: 0x0600054B RID: 1355 RVA: 0x00014664 File Offset: 0x00012864
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void SetDisplayNameNull()
			{
				base[this.tableLocalizedInfo.DisplayNameColumn] = Convert.DBNull;
			}

			// Token: 0x0400017C RID: 380
			private TextMessagingHostingData.LocalizedInfoDataTable tableLocalizedInfo;
		}

		// Token: 0x02000062 RID: 98
		public class ServicesRow : DataRow
		{
			// Token: 0x0600054C RID: 1356 RVA: 0x0001467C File Offset: 0x0001287C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal ServicesRow(DataRowBuilder rb) : base(rb)
			{
				this.tableServices = (TextMessagingHostingData.ServicesDataTable)base.Table;
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x0600054D RID: 1357 RVA: 0x00014696 File Offset: 0x00012896
			// (set) Token: 0x0600054E RID: 1358 RVA: 0x000146AE File Offset: 0x000128AE
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public int Services_Id
			{
				get
				{
					return (int)base[this.tableServices.Services_IdColumn];
				}
				set
				{
					base[this.tableServices.Services_IdColumn] = value;
				}
			}

			// Token: 0x0600054F RID: 1359 RVA: 0x000146C7 File Offset: 0x000128C7
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.ServiceRow[] GetServiceRows()
			{
				if (base.Table.ChildRelations["FK_Services_Service"] == null)
				{
					return new TextMessagingHostingData.ServiceRow[0];
				}
				return (TextMessagingHostingData.ServiceRow[])base.GetChildRows(base.Table.ChildRelations["FK_Services_Service"]);
			}

			// Token: 0x0400017D RID: 381
			private TextMessagingHostingData.ServicesDataTable tableServices;
		}

		// Token: 0x02000063 RID: 99
		public class ServiceRow : DataRow
		{
			// Token: 0x06000550 RID: 1360 RVA: 0x00014707 File Offset: 0x00012907
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal ServiceRow(DataRowBuilder rb) : base(rb)
			{
				this.tableService = (TextMessagingHostingData.ServiceDataTable)base.Table;
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000551 RID: 1361 RVA: 0x00014724 File Offset: 0x00012924
			// (set) Token: 0x06000552 RID: 1362 RVA: 0x00014768 File Offset: 0x00012968
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int Services_Id
			{
				get
				{
					int result;
					try
					{
						result = (int)base[this.tableService.Services_IdColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Services_Id' in table 'Service' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableService.Services_IdColumn] = value;
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x06000553 RID: 1363 RVA: 0x00014781 File Offset: 0x00012981
			// (set) Token: 0x06000554 RID: 1364 RVA: 0x00014799 File Offset: 0x00012999
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string RegionIso2
			{
				get
				{
					return (string)base[this.tableService.RegionIso2Column];
				}
				set
				{
					base[this.tableService.RegionIso2Column] = value;
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x06000555 RID: 1365 RVA: 0x000147AD File Offset: 0x000129AD
			// (set) Token: 0x06000556 RID: 1366 RVA: 0x000147C5 File Offset: 0x000129C5
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int CarrierIdentity
			{
				get
				{
					return (int)base[this.tableService.CarrierIdentityColumn];
				}
				set
				{
					base[this.tableService.CarrierIdentityColumn] = value;
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x06000557 RID: 1367 RVA: 0x000147DE File Offset: 0x000129DE
			// (set) Token: 0x06000558 RID: 1368 RVA: 0x000147F6 File Offset: 0x000129F6
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string Type
			{
				get
				{
					return (string)base[this.tableService.TypeColumn];
				}
				set
				{
					base[this.tableService.TypeColumn] = value;
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001480A File Offset: 0x00012A0A
			// (set) Token: 0x0600055A RID: 1370 RVA: 0x0001482C File Offset: 0x00012A2C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.ServicesRow ServicesRow
			{
				get
				{
					return (TextMessagingHostingData.ServicesRow)base.GetParentRow(base.Table.ParentRelations["FK_Services_Service"]);
				}
				set
				{
					base.SetParentRow(value, base.Table.ParentRelations["FK_Services_Service"]);
				}
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x0600055B RID: 1371 RVA: 0x0001484A File Offset: 0x00012A4A
			// (set) Token: 0x0600055C RID: 1372 RVA: 0x0001486C File Offset: 0x00012A6C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.CarrierRow CarrierRow
			{
				get
				{
					return (TextMessagingHostingData.CarrierRow)base.GetParentRow(base.Table.ParentRelations["FK_Carrier_Service"]);
				}
				set
				{
					base.SetParentRow(value, base.Table.ParentRelations["FK_Carrier_Service"]);
				}
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x0600055D RID: 1373 RVA: 0x0001488A File Offset: 0x00012A8A
			// (set) Token: 0x0600055E RID: 1374 RVA: 0x000148AC File Offset: 0x00012AAC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RegionRow RegionRow
			{
				get
				{
					return (TextMessagingHostingData.RegionRow)base.GetParentRow(base.Table.ParentRelations["FK_Region_Service"]);
				}
				set
				{
					base.SetParentRow(value, base.Table.ParentRelations["FK_Region_Service"]);
				}
			}

			// Token: 0x0600055F RID: 1375 RVA: 0x000148CA File Offset: 0x00012ACA
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsServices_IdNull()
			{
				return base.IsNull(this.tableService.Services_IdColumn);
			}

			// Token: 0x06000560 RID: 1376 RVA: 0x000148DD File Offset: 0x00012ADD
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetServices_IdNull()
			{
				base[this.tableService.Services_IdColumn] = Convert.DBNull;
			}

			// Token: 0x06000561 RID: 1377 RVA: 0x000148F5 File Offset: 0x00012AF5
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.VoiceCallForwardingRow[] GetVoiceCallForwardingRows()
			{
				if (base.Table.ChildRelations["FK_Service_VoiceCallForwarding"] == null)
				{
					return new TextMessagingHostingData.VoiceCallForwardingRow[0];
				}
				return (TextMessagingHostingData.VoiceCallForwardingRow[])base.GetChildRows(base.Table.ChildRelations["FK_Service_VoiceCallForwarding"]);
			}

			// Token: 0x06000562 RID: 1378 RVA: 0x00014935 File Offset: 0x00012B35
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.SmtpToSmsGatewayRow[] GetSmtpToSmsGatewayRows()
			{
				if (base.Table.ChildRelations["FK_Service_SmtpToSmsGateway"] == null)
				{
					return new TextMessagingHostingData.SmtpToSmsGatewayRow[0];
				}
				return (TextMessagingHostingData.SmtpToSmsGatewayRow[])base.GetChildRows(base.Table.ChildRelations["FK_Service_SmtpToSmsGateway"]);
			}

			// Token: 0x0400017E RID: 382
			private TextMessagingHostingData.ServiceDataTable tableService;
		}

		// Token: 0x02000064 RID: 100
		public class VoiceCallForwardingRow : DataRow
		{
			// Token: 0x06000563 RID: 1379 RVA: 0x00014975 File Offset: 0x00012B75
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal VoiceCallForwardingRow(DataRowBuilder rb) : base(rb)
			{
				this.tableVoiceCallForwarding = (TextMessagingHostingData.VoiceCallForwardingDataTable)base.Table;
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x06000564 RID: 1380 RVA: 0x00014990 File Offset: 0x00012B90
			// (set) Token: 0x06000565 RID: 1381 RVA: 0x000149D4 File Offset: 0x00012BD4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public string Enable
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableVoiceCallForwarding.EnableColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Enable' in table 'VoiceCallForwarding' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableVoiceCallForwarding.EnableColumn] = value;
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x06000566 RID: 1382 RVA: 0x000149E8 File Offset: 0x00012BE8
			// (set) Token: 0x06000567 RID: 1383 RVA: 0x00014A2C File Offset: 0x00012C2C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public string Disable
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableVoiceCallForwarding.DisableColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Disable' in table 'VoiceCallForwarding' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableVoiceCallForwarding.DisableColumn] = value;
				}
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x06000568 RID: 1384 RVA: 0x00014A40 File Offset: 0x00012C40
			// (set) Token: 0x06000569 RID: 1385 RVA: 0x00014A84 File Offset: 0x00012C84
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public string Type
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableVoiceCallForwarding.TypeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Type' in table 'VoiceCallForwarding' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableVoiceCallForwarding.TypeColumn] = value;
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x0600056A RID: 1386 RVA: 0x00014A98 File Offset: 0x00012C98
			// (set) Token: 0x0600056B RID: 1387 RVA: 0x00014AB0 File Offset: 0x00012CB0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string RegionIso2
			{
				get
				{
					return (string)base[this.tableVoiceCallForwarding.RegionIso2Column];
				}
				set
				{
					base[this.tableVoiceCallForwarding.RegionIso2Column] = value;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x0600056C RID: 1388 RVA: 0x00014AC4 File Offset: 0x00012CC4
			// (set) Token: 0x0600056D RID: 1389 RVA: 0x00014ADC File Offset: 0x00012CDC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int CarrierIdentity
			{
				get
				{
					return (int)base[this.tableVoiceCallForwarding.CarrierIdentityColumn];
				}
				set
				{
					base[this.tableVoiceCallForwarding.CarrierIdentityColumn] = value;
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x0600056E RID: 1390 RVA: 0x00014AF5 File Offset: 0x00012CF5
			// (set) Token: 0x0600056F RID: 1391 RVA: 0x00014B0D File Offset: 0x00012D0D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string ServiceType
			{
				get
				{
					return (string)base[this.tableVoiceCallForwarding.ServiceTypeColumn];
				}
				set
				{
					base[this.tableVoiceCallForwarding.ServiceTypeColumn] = value;
				}
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x06000570 RID: 1392 RVA: 0x00014B21 File Offset: 0x00012D21
			// (set) Token: 0x06000571 RID: 1393 RVA: 0x00014B43 File Offset: 0x00012D43
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.ServiceRow ServiceRowParent
			{
				get
				{
					return (TextMessagingHostingData.ServiceRow)base.GetParentRow(base.Table.ParentRelations["FK_Service_VoiceCallForwarding"]);
				}
				set
				{
					base.SetParentRow(value, base.Table.ParentRelations["FK_Service_VoiceCallForwarding"]);
				}
			}

			// Token: 0x06000572 RID: 1394 RVA: 0x00014B61 File Offset: 0x00012D61
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsEnableNull()
			{
				return base.IsNull(this.tableVoiceCallForwarding.EnableColumn);
			}

			// Token: 0x06000573 RID: 1395 RVA: 0x00014B74 File Offset: 0x00012D74
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void SetEnableNull()
			{
				base[this.tableVoiceCallForwarding.EnableColumn] = Convert.DBNull;
			}

			// Token: 0x06000574 RID: 1396 RVA: 0x00014B8C File Offset: 0x00012D8C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsDisableNull()
			{
				return base.IsNull(this.tableVoiceCallForwarding.DisableColumn);
			}

			// Token: 0x06000575 RID: 1397 RVA: 0x00014B9F File Offset: 0x00012D9F
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void SetDisableNull()
			{
				base[this.tableVoiceCallForwarding.DisableColumn] = Convert.DBNull;
			}

			// Token: 0x06000576 RID: 1398 RVA: 0x00014BB7 File Offset: 0x00012DB7
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsTypeNull()
			{
				return base.IsNull(this.tableVoiceCallForwarding.TypeColumn);
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x00014BCA File Offset: 0x00012DCA
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTypeNull()
			{
				base[this.tableVoiceCallForwarding.TypeColumn] = Convert.DBNull;
			}

			// Token: 0x0400017F RID: 383
			private TextMessagingHostingData.VoiceCallForwardingDataTable tableVoiceCallForwarding;
		}

		// Token: 0x02000065 RID: 101
		public class SmtpToSmsGatewayRow : DataRow
		{
			// Token: 0x06000578 RID: 1400 RVA: 0x00014BE2 File Offset: 0x00012DE2
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal SmtpToSmsGatewayRow(DataRowBuilder rb) : base(rb)
			{
				this.tableSmtpToSmsGateway = (TextMessagingHostingData.SmtpToSmsGatewayDataTable)base.Table;
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000579 RID: 1401 RVA: 0x00014BFC File Offset: 0x00012DFC
			// (set) Token: 0x0600057A RID: 1402 RVA: 0x00014C14 File Offset: 0x00012E14
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string RegionIso2
			{
				get
				{
					return (string)base[this.tableSmtpToSmsGateway.RegionIso2Column];
				}
				set
				{
					base[this.tableSmtpToSmsGateway.RegionIso2Column] = value;
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x0600057B RID: 1403 RVA: 0x00014C28 File Offset: 0x00012E28
			// (set) Token: 0x0600057C RID: 1404 RVA: 0x00014C40 File Offset: 0x00012E40
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int CarrierIdentity
			{
				get
				{
					return (int)base[this.tableSmtpToSmsGateway.CarrierIdentityColumn];
				}
				set
				{
					base[this.tableSmtpToSmsGateway.CarrierIdentityColumn] = value;
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x0600057D RID: 1405 RVA: 0x00014C59 File Offset: 0x00012E59
			// (set) Token: 0x0600057E RID: 1406 RVA: 0x00014C71 File Offset: 0x00012E71
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public string ServiceType
			{
				get
				{
					return (string)base[this.tableSmtpToSmsGateway.ServiceTypeColumn];
				}
				set
				{
					base[this.tableSmtpToSmsGateway.ServiceTypeColumn] = value;
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x0600057F RID: 1407 RVA: 0x00014C85 File Offset: 0x00012E85
			// (set) Token: 0x06000580 RID: 1408 RVA: 0x00014CA7 File Offset: 0x00012EA7
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.ServiceRow ServiceRowParent
			{
				get
				{
					return (TextMessagingHostingData.ServiceRow)base.GetParentRow(base.Table.ParentRelations["FK_Service_SmtpToSmsGateway"]);
				}
				set
				{
					base.SetParentRow(value, base.Table.ParentRelations["FK_Service_SmtpToSmsGateway"]);
				}
			}

			// Token: 0x06000581 RID: 1409 RVA: 0x00014CC5 File Offset: 0x00012EC5
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RecipientAddressingRow[] GetRecipientAddressingRows()
			{
				if (base.Table.ChildRelations["FK_SmtpToSmsGateway_RecipientAddressing"] == null)
				{
					return new TextMessagingHostingData.RecipientAddressingRow[0];
				}
				return (TextMessagingHostingData.RecipientAddressingRow[])base.GetChildRows(base.Table.ChildRelations["FK_SmtpToSmsGateway_RecipientAddressing"]);
			}

			// Token: 0x06000582 RID: 1410 RVA: 0x00014D05 File Offset: 0x00012F05
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.MessageRenderingRow[] GetMessageRenderingRows()
			{
				if (base.Table.ChildRelations["FK_SmtpToSmsGateway_MessageRendering"] == null)
				{
					return new TextMessagingHostingData.MessageRenderingRow[0];
				}
				return (TextMessagingHostingData.MessageRenderingRow[])base.GetChildRows(base.Table.ChildRelations["FK_SmtpToSmsGateway_MessageRendering"]);
			}

			// Token: 0x04000180 RID: 384
			private TextMessagingHostingData.SmtpToSmsGatewayDataTable tableSmtpToSmsGateway;
		}

		// Token: 0x02000066 RID: 102
		public class RecipientAddressingRow : DataRow
		{
			// Token: 0x06000583 RID: 1411 RVA: 0x00014D45 File Offset: 0x00012F45
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal RecipientAddressingRow(DataRowBuilder rb) : base(rb)
			{
				this.tableRecipientAddressing = (TextMessagingHostingData.RecipientAddressingDataTable)base.Table;
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x06000584 RID: 1412 RVA: 0x00014D5F File Offset: 0x00012F5F
			// (set) Token: 0x06000585 RID: 1413 RVA: 0x00014D77 File Offset: 0x00012F77
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public string RegionIso2
			{
				get
				{
					return (string)base[this.tableRecipientAddressing.RegionIso2Column];
				}
				set
				{
					base[this.tableRecipientAddressing.RegionIso2Column] = value;
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x06000586 RID: 1414 RVA: 0x00014D8C File Offset: 0x00012F8C
			// (set) Token: 0x06000587 RID: 1415 RVA: 0x00014DD0 File Offset: 0x00012FD0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public string SmtpAddress
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableRecipientAddressing.SmtpAddressColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'SmtpAddress' in table 'RecipientAddressing' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableRecipientAddressing.SmtpAddressColumn] = value;
				}
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x06000588 RID: 1416 RVA: 0x00014DE4 File Offset: 0x00012FE4
			// (set) Token: 0x06000589 RID: 1417 RVA: 0x00014DFC File Offset: 0x00012FFC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public int CarrierIdentity
			{
				get
				{
					return (int)base[this.tableRecipientAddressing.CarrierIdentityColumn];
				}
				set
				{
					base[this.tableRecipientAddressing.CarrierIdentityColumn] = value;
				}
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x0600058A RID: 1418 RVA: 0x00014E15 File Offset: 0x00013015
			// (set) Token: 0x0600058B RID: 1419 RVA: 0x00014E2D File Offset: 0x0001302D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string ServiceType
			{
				get
				{
					return (string)base[this.tableRecipientAddressing.ServiceTypeColumn];
				}
				set
				{
					base[this.tableRecipientAddressing.ServiceTypeColumn] = value;
				}
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x0600058C RID: 1420 RVA: 0x00014E41 File Offset: 0x00013041
			// (set) Token: 0x0600058D RID: 1421 RVA: 0x00014E63 File Offset: 0x00013063
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.SmtpToSmsGatewayRow SmtpToSmsGatewayRowParent
			{
				get
				{
					return (TextMessagingHostingData.SmtpToSmsGatewayRow)base.GetParentRow(base.Table.ParentRelations["FK_SmtpToSmsGateway_RecipientAddressing"]);
				}
				set
				{
					base.SetParentRow(value, base.Table.ParentRelations["FK_SmtpToSmsGateway_RecipientAddressing"]);
				}
			}

			// Token: 0x0600058E RID: 1422 RVA: 0x00014E81 File Offset: 0x00013081
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsSmtpAddressNull()
			{
				return base.IsNull(this.tableRecipientAddressing.SmtpAddressColumn);
			}

			// Token: 0x0600058F RID: 1423 RVA: 0x00014E94 File Offset: 0x00013094
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void SetSmtpAddressNull()
			{
				base[this.tableRecipientAddressing.SmtpAddressColumn] = Convert.DBNull;
			}

			// Token: 0x04000181 RID: 385
			private TextMessagingHostingData.RecipientAddressingDataTable tableRecipientAddressing;
		}

		// Token: 0x02000067 RID: 103
		public class MessageRenderingRow : DataRow
		{
			// Token: 0x06000590 RID: 1424 RVA: 0x00014EAC File Offset: 0x000130AC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal MessageRenderingRow(DataRowBuilder rb) : base(rb)
			{
				this.tableMessageRendering = (TextMessagingHostingData.MessageRenderingDataTable)base.Table;
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x06000591 RID: 1425 RVA: 0x00014EC8 File Offset: 0x000130C8
			// (set) Token: 0x06000592 RID: 1426 RVA: 0x00014F0C File Offset: 0x0001310C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public string Container
			{
				get
				{
					string result;
					try
					{
						result = (string)base[this.tableMessageRendering.ContainerColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Container' in table 'MessageRendering' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableMessageRendering.ContainerColumn] = value;
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x06000593 RID: 1427 RVA: 0x00014F20 File Offset: 0x00013120
			// (set) Token: 0x06000594 RID: 1428 RVA: 0x00014F38 File Offset: 0x00013138
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public string RegionIso2
			{
				get
				{
					return (string)base[this.tableMessageRendering.RegionIso2Column];
				}
				set
				{
					base[this.tableMessageRendering.RegionIso2Column] = value;
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06000595 RID: 1429 RVA: 0x00014F4C File Offset: 0x0001314C
			// (set) Token: 0x06000596 RID: 1430 RVA: 0x00014F64 File Offset: 0x00013164
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public int CarrierIdentity
			{
				get
				{
					return (int)base[this.tableMessageRendering.CarrierIdentityColumn];
				}
				set
				{
					base[this.tableMessageRendering.CarrierIdentityColumn] = value;
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000597 RID: 1431 RVA: 0x00014F7D File Offset: 0x0001317D
			// (set) Token: 0x06000598 RID: 1432 RVA: 0x00014F95 File Offset: 0x00013195
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public string ServiceType
			{
				get
				{
					return (string)base[this.tableMessageRendering.ServiceTypeColumn];
				}
				set
				{
					base[this.tableMessageRendering.ServiceTypeColumn] = value;
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000599 RID: 1433 RVA: 0x00014FA9 File Offset: 0x000131A9
			// (set) Token: 0x0600059A RID: 1434 RVA: 0x00014FCB File Offset: 0x000131CB
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.SmtpToSmsGatewayRow SmtpToSmsGatewayRowParent
			{
				get
				{
					return (TextMessagingHostingData.SmtpToSmsGatewayRow)base.GetParentRow(base.Table.ParentRelations["FK_SmtpToSmsGateway_MessageRendering"]);
				}
				set
				{
					base.SetParentRow(value, base.Table.ParentRelations["FK_SmtpToSmsGateway_MessageRendering"]);
				}
			}

			// Token: 0x0600059B RID: 1435 RVA: 0x00014FE9 File Offset: 0x000131E9
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsContainerNull()
			{
				return base.IsNull(this.tableMessageRendering.ContainerColumn);
			}

			// Token: 0x0600059C RID: 1436 RVA: 0x00014FFC File Offset: 0x000131FC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void SetContainerNull()
			{
				base[this.tableMessageRendering.ContainerColumn] = Convert.DBNull;
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x00015014 File Offset: 0x00013214
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.CapacityRow[] GetCapacityRows()
			{
				if (base.Table.ChildRelations["FK_MessageRendering_Capacity"] == null)
				{
					return new TextMessagingHostingData.CapacityRow[0];
				}
				return (TextMessagingHostingData.CapacityRow[])base.GetChildRows(base.Table.ChildRelations["FK_MessageRendering_Capacity"]);
			}

			// Token: 0x04000182 RID: 386
			private TextMessagingHostingData.MessageRenderingDataTable tableMessageRendering;
		}

		// Token: 0x02000068 RID: 104
		public class CapacityRow : DataRow
		{
			// Token: 0x0600059E RID: 1438 RVA: 0x00015054 File Offset: 0x00013254
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal CapacityRow(DataRowBuilder rb) : base(rb)
			{
				this.tableCapacity = (TextMessagingHostingData.CapacityDataTable)base.Table;
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001506E File Offset: 0x0001326E
			// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00015086 File Offset: 0x00013286
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string CodingScheme
			{
				get
				{
					return (string)base[this.tableCapacity.CodingSchemeColumn];
				}
				set
				{
					base[this.tableCapacity.CodingSchemeColumn] = value;
				}
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0001509C File Offset: 0x0001329C
			// (set) Token: 0x060005A2 RID: 1442 RVA: 0x000150E0 File Offset: 0x000132E0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int Capacity_Value
			{
				get
				{
					int result;
					try
					{
						result = (int)base[this.tableCapacity.Capacity_ValueColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Capacity_Value' in table 'Capacity' is DBNull.", innerException);
					}
					return result;
				}
				set
				{
					base[this.tableCapacity.Capacity_ValueColumn] = value;
				}
			}

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x060005A3 RID: 1443 RVA: 0x000150F9 File Offset: 0x000132F9
			// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00015111 File Offset: 0x00013311
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public string RegionIso2
			{
				get
				{
					return (string)base[this.tableCapacity.RegionIso2Column];
				}
				set
				{
					base[this.tableCapacity.RegionIso2Column] = value;
				}
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00015125 File Offset: 0x00013325
			// (set) Token: 0x060005A6 RID: 1446 RVA: 0x0001513D File Offset: 0x0001333D
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public int CarrierIdentity
			{
				get
				{
					return (int)base[this.tableCapacity.CarrierIdentityColumn];
				}
				set
				{
					base[this.tableCapacity.CarrierIdentityColumn] = value;
				}
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00015156 File Offset: 0x00013356
			// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0001516E File Offset: 0x0001336E
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public string ServiceType
			{
				get
				{
					return (string)base[this.tableCapacity.ServiceTypeColumn];
				}
				set
				{
					base[this.tableCapacity.ServiceTypeColumn] = value;
				}
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00015182 File Offset: 0x00013382
			// (set) Token: 0x060005AA RID: 1450 RVA: 0x000151A4 File Offset: 0x000133A4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.MessageRenderingRow MessageRenderingRowParent
			{
				get
				{
					return (TextMessagingHostingData.MessageRenderingRow)base.GetParentRow(base.Table.ParentRelations["FK_MessageRendering_Capacity"]);
				}
				set
				{
					base.SetParentRow(value, base.Table.ParentRelations["FK_MessageRendering_Capacity"]);
				}
			}

			// Token: 0x060005AB RID: 1451 RVA: 0x000151C2 File Offset: 0x000133C2
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsCapacity_ValueNull()
			{
				return base.IsNull(this.tableCapacity.Capacity_ValueColumn);
			}

			// Token: 0x060005AC RID: 1452 RVA: 0x000151D5 File Offset: 0x000133D5
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetCapacity_ValueNull()
			{
				base[this.tableCapacity.Capacity_ValueColumn] = Convert.DBNull;
			}

			// Token: 0x04000183 RID: 387
			private TextMessagingHostingData.CapacityDataTable tableCapacity;
		}

		// Token: 0x02000069 RID: 105
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class _locDefinitionRowChangeEvent : EventArgs
		{
			// Token: 0x060005AD RID: 1453 RVA: 0x000151ED File Offset: 0x000133ED
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public _locDefinitionRowChangeEvent(TextMessagingHostingData._locDefinitionRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x060005AE RID: 1454 RVA: 0x00015203 File Offset: 0x00013403
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData._locDefinitionRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001520B File Offset: 0x0001340B
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x04000184 RID: 388
			private TextMessagingHostingData._locDefinitionRow eventRow;

			// Token: 0x04000185 RID: 389
			private DataRowAction eventAction;
		}

		// Token: 0x0200006A RID: 106
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class RegionsRowChangeEvent : EventArgs
		{
			// Token: 0x060005B0 RID: 1456 RVA: 0x00015213 File Offset: 0x00013413
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public RegionsRowChangeEvent(TextMessagingHostingData.RegionsRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00015229 File Offset: 0x00013429
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RegionsRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00015231 File Offset: 0x00013431
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x04000186 RID: 390
			private TextMessagingHostingData.RegionsRow eventRow;

			// Token: 0x04000187 RID: 391
			private DataRowAction eventAction;
		}

		// Token: 0x0200006B RID: 107
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class RegionRowChangeEvent : EventArgs
		{
			// Token: 0x060005B3 RID: 1459 RVA: 0x00015239 File Offset: 0x00013439
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public RegionRowChangeEvent(TextMessagingHostingData.RegionRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001524F File Offset: 0x0001344F
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RegionRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00015257 File Offset: 0x00013457
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x04000188 RID: 392
			private TextMessagingHostingData.RegionRow eventRow;

			// Token: 0x04000189 RID: 393
			private DataRowAction eventAction;
		}

		// Token: 0x0200006C RID: 108
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class CarriersRowChangeEvent : EventArgs
		{
			// Token: 0x060005B6 RID: 1462 RVA: 0x0001525F File Offset: 0x0001345F
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public CarriersRowChangeEvent(TextMessagingHostingData.CarriersRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00015275 File Offset: 0x00013475
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.CarriersRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001527D File Offset: 0x0001347D
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x0400018A RID: 394
			private TextMessagingHostingData.CarriersRow eventRow;

			// Token: 0x0400018B RID: 395
			private DataRowAction eventAction;
		}

		// Token: 0x0200006D RID: 109
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class CarrierRowChangeEvent : EventArgs
		{
			// Token: 0x060005B9 RID: 1465 RVA: 0x00015285 File Offset: 0x00013485
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public CarrierRowChangeEvent(TextMessagingHostingData.CarrierRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001529B File Offset: 0x0001349B
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CarrierRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x060005BB RID: 1467 RVA: 0x000152A3 File Offset: 0x000134A3
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x0400018C RID: 396
			private TextMessagingHostingData.CarrierRow eventRow;

			// Token: 0x0400018D RID: 397
			private DataRowAction eventAction;
		}

		// Token: 0x0200006E RID: 110
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class LocalizedInfoRowChangeEvent : EventArgs
		{
			// Token: 0x060005BC RID: 1468 RVA: 0x000152AB File Offset: 0x000134AB
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public LocalizedInfoRowChangeEvent(TextMessagingHostingData.LocalizedInfoRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x060005BD RID: 1469 RVA: 0x000152C1 File Offset: 0x000134C1
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.LocalizedInfoRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x060005BE RID: 1470 RVA: 0x000152C9 File Offset: 0x000134C9
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x0400018E RID: 398
			private TextMessagingHostingData.LocalizedInfoRow eventRow;

			// Token: 0x0400018F RID: 399
			private DataRowAction eventAction;
		}

		// Token: 0x0200006F RID: 111
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class ServicesRowChangeEvent : EventArgs
		{
			// Token: 0x060005BF RID: 1471 RVA: 0x000152D1 File Offset: 0x000134D1
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public ServicesRowChangeEvent(TextMessagingHostingData.ServicesRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x060005C0 RID: 1472 RVA: 0x000152E7 File Offset: 0x000134E7
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.ServicesRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x060005C1 RID: 1473 RVA: 0x000152EF File Offset: 0x000134EF
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x04000190 RID: 400
			private TextMessagingHostingData.ServicesRow eventRow;

			// Token: 0x04000191 RID: 401
			private DataRowAction eventAction;
		}

		// Token: 0x02000070 RID: 112
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class ServiceRowChangeEvent : EventArgs
		{
			// Token: 0x060005C2 RID: 1474 RVA: 0x000152F7 File Offset: 0x000134F7
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public ServiceRowChangeEvent(TextMessagingHostingData.ServiceRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0001530D File Offset: 0x0001350D
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.ServiceRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00015315 File Offset: 0x00013515
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x04000192 RID: 402
			private TextMessagingHostingData.ServiceRow eventRow;

			// Token: 0x04000193 RID: 403
			private DataRowAction eventAction;
		}

		// Token: 0x02000071 RID: 113
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class VoiceCallForwardingRowChangeEvent : EventArgs
		{
			// Token: 0x060005C5 RID: 1477 RVA: 0x0001531D File Offset: 0x0001351D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public VoiceCallForwardingRowChangeEvent(TextMessagingHostingData.VoiceCallForwardingRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00015333 File Offset: 0x00013533
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.VoiceCallForwardingRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0001533B File Offset: 0x0001353B
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x04000194 RID: 404
			private TextMessagingHostingData.VoiceCallForwardingRow eventRow;

			// Token: 0x04000195 RID: 405
			private DataRowAction eventAction;
		}

		// Token: 0x02000072 RID: 114
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class SmtpToSmsGatewayRowChangeEvent : EventArgs
		{
			// Token: 0x060005C8 RID: 1480 RVA: 0x00015343 File Offset: 0x00013543
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public SmtpToSmsGatewayRowChangeEvent(TextMessagingHostingData.SmtpToSmsGatewayRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00015359 File Offset: 0x00013559
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.SmtpToSmsGatewayRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x060005CA RID: 1482 RVA: 0x00015361 File Offset: 0x00013561
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x04000196 RID: 406
			private TextMessagingHostingData.SmtpToSmsGatewayRow eventRow;

			// Token: 0x04000197 RID: 407
			private DataRowAction eventAction;
		}

		// Token: 0x02000073 RID: 115
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class RecipientAddressingRowChangeEvent : EventArgs
		{
			// Token: 0x060005CB RID: 1483 RVA: 0x00015369 File Offset: 0x00013569
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public RecipientAddressingRowChangeEvent(TextMessagingHostingData.RecipientAddressingRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001537F File Offset: 0x0001357F
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RecipientAddressingRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x060005CD RID: 1485 RVA: 0x00015387 File Offset: 0x00013587
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x04000198 RID: 408
			private TextMessagingHostingData.RecipientAddressingRow eventRow;

			// Token: 0x04000199 RID: 409
			private DataRowAction eventAction;
		}

		// Token: 0x02000074 RID: 116
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class MessageRenderingRowChangeEvent : EventArgs
		{
			// Token: 0x060005CE RID: 1486 RVA: 0x0001538F File Offset: 0x0001358F
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public MessageRenderingRowChangeEvent(TextMessagingHostingData.MessageRenderingRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x060005CF RID: 1487 RVA: 0x000153A5 File Offset: 0x000135A5
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.MessageRenderingRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x060005D0 RID: 1488 RVA: 0x000153AD File Offset: 0x000135AD
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x0400019A RID: 410
			private TextMessagingHostingData.MessageRenderingRow eventRow;

			// Token: 0x0400019B RID: 411
			private DataRowAction eventAction;
		}

		// Token: 0x02000075 RID: 117
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class CapacityRowChangeEvent : EventArgs
		{
			// Token: 0x060005D1 RID: 1489 RVA: 0x000153B5 File Offset: 0x000135B5
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public CapacityRowChangeEvent(TextMessagingHostingData.CapacityRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000153CB File Offset: 0x000135CB
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CapacityRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000153D3 File Offset: 0x000135D3
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x0400019C RID: 412
			private TextMessagingHostingData.CapacityRow eventRow;

			// Token: 0x0400019D RID: 413
			private DataRowAction eventAction;
		}
	}
}
