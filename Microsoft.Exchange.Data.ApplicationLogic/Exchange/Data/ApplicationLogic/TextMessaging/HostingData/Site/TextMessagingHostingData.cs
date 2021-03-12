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

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.HostingData.Site
{
	// Token: 0x02000010 RID: 16
	[XmlSchemaProvider("GetTypedDataSetSchema")]
	[XmlRoot("TextMessagingHostingData")]
	[ToolboxItem(true)]
	[HelpKeyword("vs.data.DataSet")]
	[DesignerCategory("code")]
	[Serializable]
	internal class TextMessagingHostingData : DataSet
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00003960 File Offset: 0x00001B60
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public TextMessagingHostingData()
		{
			base.BeginInit();
			this.InitClass();
			CollectionChangeEventHandler value = new CollectionChangeEventHandler(this.SchemaChanged);
			base.Tables.CollectionChanged += value;
			base.Relations.CollectionChanged += value;
			base.EndInit();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000039B4 File Offset: 0x00001BB4
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003D37 File Offset: 0x00001F37
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DebuggerNonUserCode]
		[Browsable(false)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public TextMessagingHostingData.RegionsDataTable Regions
		{
			get
			{
				return this.tableRegions;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003D3F File Offset: 0x00001F3F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public TextMessagingHostingData.RegionDataTable Region
		{
			get
			{
				return this.tableRegion;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003D47 File Offset: 0x00001F47
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[Browsable(false)]
		[DebuggerNonUserCode]
		public TextMessagingHostingData.CarriersDataTable Carriers
		{
			get
			{
				return this.tableCarriers;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003D4F File Offset: 0x00001F4F
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003D57 File Offset: 0x00001F57
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public TextMessagingHostingData.LocalizedInfoDataTable LocalizedInfo
		{
			get
			{
				return this.tableLocalizedInfo;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003D5F File Offset: 0x00001F5F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public TextMessagingHostingData.ServicesDataTable Services
		{
			get
			{
				return this.tableServices;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003D67 File Offset: 0x00001F67
		[Browsable(false)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DebuggerNonUserCode]
		public TextMessagingHostingData.ServiceDataTable Service
		{
			get
			{
				return this.tableService;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003D6F File Offset: 0x00001F6F
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[Browsable(false)]
		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TextMessagingHostingData.VoiceCallForwardingDataTable VoiceCallForwarding
		{
			get
			{
				return this.tableVoiceCallForwarding;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003D77 File Offset: 0x00001F77
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TextMessagingHostingData.SmtpToSmsGatewayDataTable SmtpToSmsGateway
		{
			get
			{
				return this.tableSmtpToSmsGateway;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003D7F File Offset: 0x00001F7F
		[Browsable(false)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TextMessagingHostingData.RecipientAddressingDataTable RecipientAddressing
		{
			get
			{
				return this.tableRecipientAddressing;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003D87 File Offset: 0x00001F87
		[Browsable(false)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TextMessagingHostingData.MessageRenderingDataTable MessageRendering
		{
			get
			{
				return this.tableMessageRendering;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003D8F File Offset: 0x00001F8F
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003D97 File Offset: 0x00001F97
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003D9F File Offset: 0x00001F9F
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DebuggerNonUserCode]
		[Browsable(true)]
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003DA8 File Offset: 0x00001FA8
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DebuggerNonUserCode]
		public new DataTableCollection Tables
		{
			get
			{
				return base.Tables;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003DB0 File Offset: 0x00001FB0
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DebuggerNonUserCode]
		public new DataRelationCollection Relations
		{
			get
			{
				return base.Relations;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003DB8 File Offset: 0x00001FB8
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void InitializeDerivedDataSet()
		{
			base.BeginInit();
			this.InitClass();
			base.EndInit();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003DCC File Offset: 0x00001FCC
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public override DataSet Clone()
		{
			TextMessagingHostingData textMessagingHostingData = (TextMessagingHostingData)base.Clone();
			textMessagingHostingData.InitVars();
			textMessagingHostingData.SchemaSerializationMode = this.SchemaSerializationMode;
			return textMessagingHostingData;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003DF8 File Offset: 0x00001FF8
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected override bool ShouldSerializeTables()
		{
			return false;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003DFB File Offset: 0x00001FFB
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected override bool ShouldSerializeRelations()
		{
			return false;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003E00 File Offset: 0x00002000
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected override void ReadXmlSerializable(XmlReader reader)
		{
			if (base.DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
			{
				this.Reset();
				DataSet dataSet = new DataSet();
				dataSet.ReadXml(reader);
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

		// Token: 0x060000B1 RID: 177 RVA: 0x000040EC File Offset: 0x000022EC
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override XmlSchema GetSchemaSerializable()
		{
			MemoryStream memoryStream = new MemoryStream();
			base.WriteXmlSchema(new XmlTextWriter(memoryStream, null));
			memoryStream.Position = 0L;
			return XmlSchema.Read(new XmlTextReader(memoryStream), null);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004120 File Offset: 0x00002320
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		internal void InitVars()
		{
			this.InitVars(true);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000412C File Offset: 0x0000232C
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		internal void InitVars(bool initTable)
		{
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
			this.relationFK_Service_VoiceCallForwarding = this.Relations["FK_Service_VoiceCallForwarding"];
			this.relationFK_Service_SmtpToSmsGateway = this.Relations["FK_Service_SmtpToSmsGateway"];
			this.relationFK_SmtpToSmsGateway_RecipientAddressing = this.Relations["FK_SmtpToSmsGateway_RecipientAddressing"];
			this.relationFK_SmtpToSmsGateway_MessageRendering = this.Relations["FK_SmtpToSmsGateway_MessageRendering"];
			this.relationFK_MessageRendering_Capacity = this.Relations["FK_MessageRendering_Capacity"];
			this.relationFK_Carrier_Service = this.Relations["FK_Carrier_Service"];
			this.relationFK_Region_Service = this.Relations["FK_Region_Service"];
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004478 File Offset: 0x00002678
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private void InitClass()
		{
			base.DataSetName = "TextMessagingHostingData";
			base.Prefix = "";
			base.Locale = new CultureInfo("");
			base.EnforceConstraints = true;
			this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
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
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004FDB File Offset: 0x000031DB
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializeRegions()
		{
			return false;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004FDE File Offset: 0x000031DE
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private bool ShouldSerializeRegion()
		{
			return false;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004FE1 File Offset: 0x000031E1
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializeCarriers()
		{
			return false;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004FE4 File Offset: 0x000031E4
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializeCarrier()
		{
			return false;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004FE7 File Offset: 0x000031E7
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializeLocalizedInfo()
		{
			return false;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004FEA File Offset: 0x000031EA
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private bool ShouldSerializeServices()
		{
			return false;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004FED File Offset: 0x000031ED
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializeService()
		{
			return false;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004FF0 File Offset: 0x000031F0
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializeVoiceCallForwarding()
		{
			return false;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004FF3 File Offset: 0x000031F3
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializeSmtpToSmsGateway()
		{
			return false;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004FF6 File Offset: 0x000031F6
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializeRecipientAddressing()
		{
			return false;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004FF9 File Offset: 0x000031F9
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializeMessageRendering()
		{
			return false;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004FFC File Offset: 0x000031FC
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private bool ShouldSerializeCapacity()
		{
			return false;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004FFF File Offset: 0x000031FF
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private void SchemaChanged(object sender, CollectionChangeEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Remove)
			{
				this.InitVars();
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005010 File Offset: 0x00003210
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

		// Token: 0x04000070 RID: 112
		private TextMessagingHostingData.RegionsDataTable tableRegions;

		// Token: 0x04000071 RID: 113
		private TextMessagingHostingData.RegionDataTable tableRegion;

		// Token: 0x04000072 RID: 114
		private TextMessagingHostingData.CarriersDataTable tableCarriers;

		// Token: 0x04000073 RID: 115
		private TextMessagingHostingData.CarrierDataTable tableCarrier;

		// Token: 0x04000074 RID: 116
		private TextMessagingHostingData.LocalizedInfoDataTable tableLocalizedInfo;

		// Token: 0x04000075 RID: 117
		private TextMessagingHostingData.ServicesDataTable tableServices;

		// Token: 0x04000076 RID: 118
		private TextMessagingHostingData.ServiceDataTable tableService;

		// Token: 0x04000077 RID: 119
		private TextMessagingHostingData.VoiceCallForwardingDataTable tableVoiceCallForwarding;

		// Token: 0x04000078 RID: 120
		private TextMessagingHostingData.SmtpToSmsGatewayDataTable tableSmtpToSmsGateway;

		// Token: 0x04000079 RID: 121
		private TextMessagingHostingData.RecipientAddressingDataTable tableRecipientAddressing;

		// Token: 0x0400007A RID: 122
		private TextMessagingHostingData.MessageRenderingDataTable tableMessageRendering;

		// Token: 0x0400007B RID: 123
		private TextMessagingHostingData.CapacityDataTable tableCapacity;

		// Token: 0x0400007C RID: 124
		private DataRelation relationRegions_Region;

		// Token: 0x0400007D RID: 125
		private DataRelation relationCarriers_Carrier;

		// Token: 0x0400007E RID: 126
		private DataRelation relationCarrier_LocalizedInfo;

		// Token: 0x0400007F RID: 127
		private DataRelation relationFK_Services_Service;

		// Token: 0x04000080 RID: 128
		private DataRelation relationFK_Service_VoiceCallForwarding;

		// Token: 0x04000081 RID: 129
		private DataRelation relationFK_Service_SmtpToSmsGateway;

		// Token: 0x04000082 RID: 130
		private DataRelation relationFK_SmtpToSmsGateway_RecipientAddressing;

		// Token: 0x04000083 RID: 131
		private DataRelation relationFK_SmtpToSmsGateway_MessageRendering;

		// Token: 0x04000084 RID: 132
		private DataRelation relationFK_MessageRendering_Capacity;

		// Token: 0x04000085 RID: 133
		private DataRelation relationFK_Carrier_Service;

		// Token: 0x04000086 RID: 134
		private DataRelation relationFK_Region_Service;

		// Token: 0x04000087 RID: 135
		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x060000C4 RID: 196
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void RegionsRowChangeEventHandler(object sender, TextMessagingHostingData.RegionsRowChangeEvent e);

		// Token: 0x02000012 RID: 18
		// (Invoke) Token: 0x060000C8 RID: 200
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void RegionRowChangeEventHandler(object sender, TextMessagingHostingData.RegionRowChangeEvent e);

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x060000CC RID: 204
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void CarriersRowChangeEventHandler(object sender, TextMessagingHostingData.CarriersRowChangeEvent e);

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x060000D0 RID: 208
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void CarrierRowChangeEventHandler(object sender, TextMessagingHostingData.CarrierRowChangeEvent e);

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x060000D4 RID: 212
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void LocalizedInfoRowChangeEventHandler(object sender, TextMessagingHostingData.LocalizedInfoRowChangeEvent e);

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x060000D8 RID: 216
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void ServicesRowChangeEventHandler(object sender, TextMessagingHostingData.ServicesRowChangeEvent e);

		// Token: 0x02000017 RID: 23
		// (Invoke) Token: 0x060000DC RID: 220
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void ServiceRowChangeEventHandler(object sender, TextMessagingHostingData.ServiceRowChangeEvent e);

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x060000E0 RID: 224
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void VoiceCallForwardingRowChangeEventHandler(object sender, TextMessagingHostingData.VoiceCallForwardingRowChangeEvent e);

		// Token: 0x02000019 RID: 25
		// (Invoke) Token: 0x060000E4 RID: 228
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void SmtpToSmsGatewayRowChangeEventHandler(object sender, TextMessagingHostingData.SmtpToSmsGatewayRowChangeEvent e);

		// Token: 0x0200001A RID: 26
		// (Invoke) Token: 0x060000E8 RID: 232
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void RecipientAddressingRowChangeEventHandler(object sender, TextMessagingHostingData.RecipientAddressingRowChangeEvent e);

		// Token: 0x0200001B RID: 27
		// (Invoke) Token: 0x060000EC RID: 236
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void MessageRenderingRowChangeEventHandler(object sender, TextMessagingHostingData.MessageRenderingRowChangeEvent e);

		// Token: 0x0200001C RID: 28
		// (Invoke) Token: 0x060000F0 RID: 240
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void CapacityRowChangeEventHandler(object sender, TextMessagingHostingData.CapacityRowChangeEvent e);

		// Token: 0x0200001D RID: 29
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class RegionsDataTable : TypedTableBase<TextMessagingHostingData.RegionsRow>
		{
			// Token: 0x060000F3 RID: 243 RVA: 0x00005158 File Offset: 0x00003358
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public RegionsDataTable()
			{
				base.TableName = "Regions";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x060000F4 RID: 244 RVA: 0x00005180 File Offset: 0x00003380
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

			// Token: 0x060000F5 RID: 245 RVA: 0x00005228 File Offset: 0x00003428
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected RegionsDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005238 File Offset: 0x00003438
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn Regions_IdColumn
			{
				get
				{
					return this.columnRegions_Id;
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005240 File Offset: 0x00003440
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

			// Token: 0x17000056 RID: 86
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RegionsRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.RegionsRow)base.Rows[index];
				}
			}

			// Token: 0x14000001 RID: 1
			// (add) Token: 0x060000F9 RID: 249 RVA: 0x00005260 File Offset: 0x00003460
			// (remove) Token: 0x060000FA RID: 250 RVA: 0x00005298 File Offset: 0x00003498
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionsRowChangeEventHandler RegionsRowChanging;

			// Token: 0x14000002 RID: 2
			// (add) Token: 0x060000FB RID: 251 RVA: 0x000052D0 File Offset: 0x000034D0
			// (remove) Token: 0x060000FC RID: 252 RVA: 0x00005308 File Offset: 0x00003508
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionsRowChangeEventHandler RegionsRowChanged;

			// Token: 0x14000003 RID: 3
			// (add) Token: 0x060000FD RID: 253 RVA: 0x00005340 File Offset: 0x00003540
			// (remove) Token: 0x060000FE RID: 254 RVA: 0x00005378 File Offset: 0x00003578
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionsRowChangeEventHandler RegionsRowDeleting;

			// Token: 0x14000004 RID: 4
			// (add) Token: 0x060000FF RID: 255 RVA: 0x000053B0 File Offset: 0x000035B0
			// (remove) Token: 0x06000100 RID: 256 RVA: 0x000053E8 File Offset: 0x000035E8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionsRowChangeEventHandler RegionsRowDeleted;

			// Token: 0x06000101 RID: 257 RVA: 0x0000541D File Offset: 0x0000361D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void AddRegionsRow(TextMessagingHostingData.RegionsRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x06000102 RID: 258 RVA: 0x0000542C File Offset: 0x0000362C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RegionsRow AddRegionsRow()
			{
				TextMessagingHostingData.RegionsRow regionsRow = (TextMessagingHostingData.RegionsRow)base.NewRow();
				object[] array = new object[1];
				object[] itemArray = array;
				regionsRow.ItemArray = itemArray;
				base.Rows.Add(regionsRow);
				return regionsRow;
			}

			// Token: 0x06000103 RID: 259 RVA: 0x00005464 File Offset: 0x00003664
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				TextMessagingHostingData.RegionsDataTable regionsDataTable = (TextMessagingHostingData.RegionsDataTable)base.Clone();
				regionsDataTable.InitVars();
				return regionsDataTable;
			}

			// Token: 0x06000104 RID: 260 RVA: 0x00005484 File Offset: 0x00003684
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.RegionsDataTable();
			}

			// Token: 0x06000105 RID: 261 RVA: 0x0000548B File Offset: 0x0000368B
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnRegions_Id = base.Columns["Regions_Id"];
			}

			// Token: 0x06000106 RID: 262 RVA: 0x000054A4 File Offset: 0x000036A4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x06000107 RID: 263 RVA: 0x00005539 File Offset: 0x00003739
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RegionsRow NewRegionsRow()
			{
				return (TextMessagingHostingData.RegionsRow)base.NewRow();
			}

			// Token: 0x06000108 RID: 264 RVA: 0x00005546 File Offset: 0x00003746
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.RegionsRow(builder);
			}

			// Token: 0x06000109 RID: 265 RVA: 0x0000554E File Offset: 0x0000374E
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.RegionsRow);
			}

			// Token: 0x0600010A RID: 266 RVA: 0x0000555A File Offset: 0x0000375A
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.RegionsRowChanged != null)
				{
					this.RegionsRowChanged(this, new TextMessagingHostingData.RegionsRowChangeEvent((TextMessagingHostingData.RegionsRow)e.Row, e.Action));
				}
			}

			// Token: 0x0600010B RID: 267 RVA: 0x0000558D File Offset: 0x0000378D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.RegionsRowChanging != null)
				{
					this.RegionsRowChanging(this, new TextMessagingHostingData.RegionsRowChangeEvent((TextMessagingHostingData.RegionsRow)e.Row, e.Action));
				}
			}

			// Token: 0x0600010C RID: 268 RVA: 0x000055C0 File Offset: 0x000037C0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.RegionsRowDeleted != null)
				{
					this.RegionsRowDeleted(this, new TextMessagingHostingData.RegionsRowChangeEvent((TextMessagingHostingData.RegionsRow)e.Row, e.Action));
				}
			}

			// Token: 0x0600010D RID: 269 RVA: 0x000055F3 File Offset: 0x000037F3
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

			// Token: 0x0600010E RID: 270 RVA: 0x00005626 File Offset: 0x00003826
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveRegionsRow(TextMessagingHostingData.RegionsRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x0600010F RID: 271 RVA: 0x00005634 File Offset: 0x00003834
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

			// Token: 0x04000088 RID: 136
			private DataColumn columnRegions_Id;
		}

		// Token: 0x0200001E RID: 30
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class RegionDataTable : TypedTableBase<TextMessagingHostingData.RegionRow>
		{
			// Token: 0x06000110 RID: 272 RVA: 0x0000582C File Offset: 0x00003A2C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public RegionDataTable()
			{
				base.TableName = "Region";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x06000111 RID: 273 RVA: 0x00005854 File Offset: 0x00003A54
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

			// Token: 0x06000112 RID: 274 RVA: 0x000058FC File Offset: 0x00003AFC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected RegionDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000113 RID: 275 RVA: 0x0000590C File Offset: 0x00003B0C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CountryCodeColumn
			{
				get
				{
					return this.columnCountryCode;
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000114 RID: 276 RVA: 0x00005914 File Offset: 0x00003B14
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn PhoneNumberExampleColumn
			{
				get
				{
					return this.columnPhoneNumberExample;
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000115 RID: 277 RVA: 0x0000591C File Offset: 0x00003B1C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn Iso2Column
			{
				get
				{
					return this.columnIso2;
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000116 RID: 278 RVA: 0x00005924 File Offset: 0x00003B24
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn Regions_IdColumn
			{
				get
				{
					return this.columnRegions_Id;
				}
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000117 RID: 279 RVA: 0x0000592C File Offset: 0x00003B2C
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

			// Token: 0x1700005C RID: 92
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RegionRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.RegionRow)base.Rows[index];
				}
			}

			// Token: 0x14000005 RID: 5
			// (add) Token: 0x06000119 RID: 281 RVA: 0x0000594C File Offset: 0x00003B4C
			// (remove) Token: 0x0600011A RID: 282 RVA: 0x00005984 File Offset: 0x00003B84
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionRowChangeEventHandler RegionRowChanging;

			// Token: 0x14000006 RID: 6
			// (add) Token: 0x0600011B RID: 283 RVA: 0x000059BC File Offset: 0x00003BBC
			// (remove) Token: 0x0600011C RID: 284 RVA: 0x000059F4 File Offset: 0x00003BF4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionRowChangeEventHandler RegionRowChanged;

			// Token: 0x14000007 RID: 7
			// (add) Token: 0x0600011D RID: 285 RVA: 0x00005A2C File Offset: 0x00003C2C
			// (remove) Token: 0x0600011E RID: 286 RVA: 0x00005A64 File Offset: 0x00003C64
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionRowChangeEventHandler RegionRowDeleting;

			// Token: 0x14000008 RID: 8
			// (add) Token: 0x0600011F RID: 287 RVA: 0x00005A9C File Offset: 0x00003C9C
			// (remove) Token: 0x06000120 RID: 288 RVA: 0x00005AD4 File Offset: 0x00003CD4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RegionRowChangeEventHandler RegionRowDeleted;

			// Token: 0x06000121 RID: 289 RVA: 0x00005B09 File Offset: 0x00003D09
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void AddRegionRow(TextMessagingHostingData.RegionRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x06000122 RID: 290 RVA: 0x00005B18 File Offset: 0x00003D18
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

			// Token: 0x06000123 RID: 291 RVA: 0x00005B6C File Offset: 0x00003D6C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RegionRow FindByIso2(string Iso2)
			{
				return (TextMessagingHostingData.RegionRow)base.Rows.Find(new object[]
				{
					Iso2
				});
			}

			// Token: 0x06000124 RID: 292 RVA: 0x00005B98 File Offset: 0x00003D98
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				TextMessagingHostingData.RegionDataTable regionDataTable = (TextMessagingHostingData.RegionDataTable)base.Clone();
				regionDataTable.InitVars();
				return regionDataTable;
			}

			// Token: 0x06000125 RID: 293 RVA: 0x00005BB8 File Offset: 0x00003DB8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.RegionDataTable();
			}

			// Token: 0x06000126 RID: 294 RVA: 0x00005BC0 File Offset: 0x00003DC0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal void InitVars()
			{
				this.columnCountryCode = base.Columns["CountryCode"];
				this.columnPhoneNumberExample = base.Columns["PhoneNumberExample"];
				this.columnIso2 = base.Columns["Iso2"];
				this.columnRegions_Id = base.Columns["Regions_Id"];
			}

			// Token: 0x06000127 RID: 295 RVA: 0x00005C28 File Offset: 0x00003E28
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

			// Token: 0x06000128 RID: 296 RVA: 0x00005D54 File Offset: 0x00003F54
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RegionRow NewRegionRow()
			{
				return (TextMessagingHostingData.RegionRow)base.NewRow();
			}

			// Token: 0x06000129 RID: 297 RVA: 0x00005D61 File Offset: 0x00003F61
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.RegionRow(builder);
			}

			// Token: 0x0600012A RID: 298 RVA: 0x00005D69 File Offset: 0x00003F69
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.RegionRow);
			}

			// Token: 0x0600012B RID: 299 RVA: 0x00005D75 File Offset: 0x00003F75
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

			// Token: 0x0600012C RID: 300 RVA: 0x00005DA8 File Offset: 0x00003FA8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.RegionRowChanging != null)
				{
					this.RegionRowChanging(this, new TextMessagingHostingData.RegionRowChangeEvent((TextMessagingHostingData.RegionRow)e.Row, e.Action));
				}
			}

			// Token: 0x0600012D RID: 301 RVA: 0x00005DDB File Offset: 0x00003FDB
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.RegionRowDeleted != null)
				{
					this.RegionRowDeleted(this, new TextMessagingHostingData.RegionRowChangeEvent((TextMessagingHostingData.RegionRow)e.Row, e.Action));
				}
			}

			// Token: 0x0600012E RID: 302 RVA: 0x00005E0E File Offset: 0x0000400E
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

			// Token: 0x0600012F RID: 303 RVA: 0x00005E41 File Offset: 0x00004041
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveRegionRow(TextMessagingHostingData.RegionRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x06000130 RID: 304 RVA: 0x00005E50 File Offset: 0x00004050
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

			// Token: 0x0400008D RID: 141
			private DataColumn columnCountryCode;

			// Token: 0x0400008E RID: 142
			private DataColumn columnPhoneNumberExample;

			// Token: 0x0400008F RID: 143
			private DataColumn columnIso2;

			// Token: 0x04000090 RID: 144
			private DataColumn columnRegions_Id;
		}

		// Token: 0x0200001F RID: 31
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class CarriersDataTable : TypedTableBase<TextMessagingHostingData.CarriersRow>
		{
			// Token: 0x06000131 RID: 305 RVA: 0x00006048 File Offset: 0x00004248
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public CarriersDataTable()
			{
				base.TableName = "Carriers";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x06000132 RID: 306 RVA: 0x00006070 File Offset: 0x00004270
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

			// Token: 0x06000133 RID: 307 RVA: 0x00006118 File Offset: 0x00004318
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected CarriersDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000134 RID: 308 RVA: 0x00006128 File Offset: 0x00004328
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn Carriers_IdColumn
			{
				get
				{
					return this.columnCarriers_Id;
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x06000135 RID: 309 RVA: 0x00006130 File Offset: 0x00004330
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

			// Token: 0x1700005F RID: 95
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.CarriersRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.CarriersRow)base.Rows[index];
				}
			}

			// Token: 0x14000009 RID: 9
			// (add) Token: 0x06000137 RID: 311 RVA: 0x00006150 File Offset: 0x00004350
			// (remove) Token: 0x06000138 RID: 312 RVA: 0x00006188 File Offset: 0x00004388
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarriersRowChangeEventHandler CarriersRowChanging;

			// Token: 0x1400000A RID: 10
			// (add) Token: 0x06000139 RID: 313 RVA: 0x000061C0 File Offset: 0x000043C0
			// (remove) Token: 0x0600013A RID: 314 RVA: 0x000061F8 File Offset: 0x000043F8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarriersRowChangeEventHandler CarriersRowChanged;

			// Token: 0x1400000B RID: 11
			// (add) Token: 0x0600013B RID: 315 RVA: 0x00006230 File Offset: 0x00004430
			// (remove) Token: 0x0600013C RID: 316 RVA: 0x00006268 File Offset: 0x00004468
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarriersRowChangeEventHandler CarriersRowDeleting;

			// Token: 0x1400000C RID: 12
			// (add) Token: 0x0600013D RID: 317 RVA: 0x000062A0 File Offset: 0x000044A0
			// (remove) Token: 0x0600013E RID: 318 RVA: 0x000062D8 File Offset: 0x000044D8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarriersRowChangeEventHandler CarriersRowDeleted;

			// Token: 0x0600013F RID: 319 RVA: 0x0000630D File Offset: 0x0000450D
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void AddCarriersRow(TextMessagingHostingData.CarriersRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x06000140 RID: 320 RVA: 0x0000631C File Offset: 0x0000451C
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

			// Token: 0x06000141 RID: 321 RVA: 0x00006354 File Offset: 0x00004554
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				TextMessagingHostingData.CarriersDataTable carriersDataTable = (TextMessagingHostingData.CarriersDataTable)base.Clone();
				carriersDataTable.InitVars();
				return carriersDataTable;
			}

			// Token: 0x06000142 RID: 322 RVA: 0x00006374 File Offset: 0x00004574
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.CarriersDataTable();
			}

			// Token: 0x06000143 RID: 323 RVA: 0x0000637B File Offset: 0x0000457B
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal void InitVars()
			{
				this.columnCarriers_Id = base.Columns["Carriers_Id"];
			}

			// Token: 0x06000144 RID: 324 RVA: 0x00006394 File Offset: 0x00004594
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

			// Token: 0x06000145 RID: 325 RVA: 0x00006429 File Offset: 0x00004629
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.CarriersRow NewCarriersRow()
			{
				return (TextMessagingHostingData.CarriersRow)base.NewRow();
			}

			// Token: 0x06000146 RID: 326 RVA: 0x00006436 File Offset: 0x00004636
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.CarriersRow(builder);
			}

			// Token: 0x06000147 RID: 327 RVA: 0x0000643E File Offset: 0x0000463E
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.CarriersRow);
			}

			// Token: 0x06000148 RID: 328 RVA: 0x0000644A File Offset: 0x0000464A
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

			// Token: 0x06000149 RID: 329 RVA: 0x0000647D File Offset: 0x0000467D
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.CarriersRowChanging != null)
				{
					this.CarriersRowChanging(this, new TextMessagingHostingData.CarriersRowChangeEvent((TextMessagingHostingData.CarriersRow)e.Row, e.Action));
				}
			}

			// Token: 0x0600014A RID: 330 RVA: 0x000064B0 File Offset: 0x000046B0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.CarriersRowDeleted != null)
				{
					this.CarriersRowDeleted(this, new TextMessagingHostingData.CarriersRowChangeEvent((TextMessagingHostingData.CarriersRow)e.Row, e.Action));
				}
			}

			// Token: 0x0600014B RID: 331 RVA: 0x000064E3 File Offset: 0x000046E3
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

			// Token: 0x0600014C RID: 332 RVA: 0x00006516 File Offset: 0x00004716
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void RemoveCarriersRow(TextMessagingHostingData.CarriersRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x0600014D RID: 333 RVA: 0x00006524 File Offset: 0x00004724
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

			// Token: 0x04000095 RID: 149
			private DataColumn columnCarriers_Id;
		}

		// Token: 0x02000020 RID: 32
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class CarrierDataTable : TypedTableBase<TextMessagingHostingData.CarrierRow>
		{
			// Token: 0x0600014E RID: 334 RVA: 0x0000671C File Offset: 0x0000491C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public CarrierDataTable()
			{
				base.TableName = "Carrier";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x0600014F RID: 335 RVA: 0x00006744 File Offset: 0x00004944
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x06000150 RID: 336 RVA: 0x000067EC File Offset: 0x000049EC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected CarrierDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x06000151 RID: 337 RVA: 0x000067FC File Offset: 0x000049FC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn IdentityColumn
			{
				get
				{
					return this.columnIdentity;
				}
			}

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000152 RID: 338 RVA: 0x00006804 File Offset: 0x00004A04
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn Carriers_IdColumn
			{
				get
				{
					return this.columnCarriers_Id;
				}
			}

			// Token: 0x17000062 RID: 98
			// (get) Token: 0x06000153 RID: 339 RVA: 0x0000680C File Offset: 0x00004A0C
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

			// Token: 0x17000063 RID: 99
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CarrierRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.CarrierRow)base.Rows[index];
				}
			}

			// Token: 0x1400000D RID: 13
			// (add) Token: 0x06000155 RID: 341 RVA: 0x0000682C File Offset: 0x00004A2C
			// (remove) Token: 0x06000156 RID: 342 RVA: 0x00006864 File Offset: 0x00004A64
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarrierRowChangeEventHandler CarrierRowChanging;

			// Token: 0x1400000E RID: 14
			// (add) Token: 0x06000157 RID: 343 RVA: 0x0000689C File Offset: 0x00004A9C
			// (remove) Token: 0x06000158 RID: 344 RVA: 0x000068D4 File Offset: 0x00004AD4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarrierRowChangeEventHandler CarrierRowChanged;

			// Token: 0x1400000F RID: 15
			// (add) Token: 0x06000159 RID: 345 RVA: 0x0000690C File Offset: 0x00004B0C
			// (remove) Token: 0x0600015A RID: 346 RVA: 0x00006944 File Offset: 0x00004B44
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarrierRowChangeEventHandler CarrierRowDeleting;

			// Token: 0x14000010 RID: 16
			// (add) Token: 0x0600015B RID: 347 RVA: 0x0000697C File Offset: 0x00004B7C
			// (remove) Token: 0x0600015C RID: 348 RVA: 0x000069B4 File Offset: 0x00004BB4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CarrierRowChangeEventHandler CarrierRowDeleted;

			// Token: 0x0600015D RID: 349 RVA: 0x000069E9 File Offset: 0x00004BE9
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void AddCarrierRow(TextMessagingHostingData.CarrierRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x0600015E RID: 350 RVA: 0x000069F8 File Offset: 0x00004BF8
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

			// Token: 0x0600015F RID: 351 RVA: 0x00006A44 File Offset: 0x00004C44
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CarrierRow FindByIdentity(int Identity)
			{
				return (TextMessagingHostingData.CarrierRow)base.Rows.Find(new object[]
				{
					Identity
				});
			}

			// Token: 0x06000160 RID: 352 RVA: 0x00006A74 File Offset: 0x00004C74
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				TextMessagingHostingData.CarrierDataTable carrierDataTable = (TextMessagingHostingData.CarrierDataTable)base.Clone();
				carrierDataTable.InitVars();
				return carrierDataTable;
			}

			// Token: 0x06000161 RID: 353 RVA: 0x00006A94 File Offset: 0x00004C94
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.CarrierDataTable();
			}

			// Token: 0x06000162 RID: 354 RVA: 0x00006A9B File Offset: 0x00004C9B
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnIdentity = base.Columns["Identity"];
				this.columnCarriers_Id = base.Columns["Carriers_Id"];
			}

			// Token: 0x06000163 RID: 355 RVA: 0x00006ACC File Offset: 0x00004CCC
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

			// Token: 0x06000164 RID: 356 RVA: 0x00006B92 File Offset: 0x00004D92
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.CarrierRow NewCarrierRow()
			{
				return (TextMessagingHostingData.CarrierRow)base.NewRow();
			}

			// Token: 0x06000165 RID: 357 RVA: 0x00006B9F File Offset: 0x00004D9F
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.CarrierRow(builder);
			}

			// Token: 0x06000166 RID: 358 RVA: 0x00006BA7 File Offset: 0x00004DA7
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.CarrierRow);
			}

			// Token: 0x06000167 RID: 359 RVA: 0x00006BB3 File Offset: 0x00004DB3
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.CarrierRowChanged != null)
				{
					this.CarrierRowChanged(this, new TextMessagingHostingData.CarrierRowChangeEvent((TextMessagingHostingData.CarrierRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000168 RID: 360 RVA: 0x00006BE6 File Offset: 0x00004DE6
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

			// Token: 0x06000169 RID: 361 RVA: 0x00006C19 File Offset: 0x00004E19
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.CarrierRowDeleted != null)
				{
					this.CarrierRowDeleted(this, new TextMessagingHostingData.CarrierRowChangeEvent((TextMessagingHostingData.CarrierRow)e.Row, e.Action));
				}
			}

			// Token: 0x0600016A RID: 362 RVA: 0x00006C4C File Offset: 0x00004E4C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.CarrierRowDeleting != null)
				{
					this.CarrierRowDeleting(this, new TextMessagingHostingData.CarrierRowChangeEvent((TextMessagingHostingData.CarrierRow)e.Row, e.Action));
				}
			}

			// Token: 0x0600016B RID: 363 RVA: 0x00006C7F File Offset: 0x00004E7F
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveCarrierRow(TextMessagingHostingData.CarrierRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x0600016C RID: 364 RVA: 0x00006C90 File Offset: 0x00004E90
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

			// Token: 0x0400009A RID: 154
			private DataColumn columnIdentity;

			// Token: 0x0400009B RID: 155
			private DataColumn columnCarriers_Id;
		}

		// Token: 0x02000021 RID: 33
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class LocalizedInfoDataTable : TypedTableBase<TextMessagingHostingData.LocalizedInfoRow>
		{
			// Token: 0x0600016D RID: 365 RVA: 0x00006E88 File Offset: 0x00005088
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public LocalizedInfoDataTable()
			{
				base.TableName = "LocalizedInfo";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x0600016E RID: 366 RVA: 0x00006EB0 File Offset: 0x000050B0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

			// Token: 0x0600016F RID: 367 RVA: 0x00006F58 File Offset: 0x00005158
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected LocalizedInfoDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x06000170 RID: 368 RVA: 0x00006F68 File Offset: 0x00005168
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn DisplayNameColumn
			{
				get
				{
					return this.columnDisplayName;
				}
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000171 RID: 369 RVA: 0x00006F70 File Offset: 0x00005170
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn CultureColumn
			{
				get
				{
					return this.columnCulture;
				}
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000172 RID: 370 RVA: 0x00006F78 File Offset: 0x00005178
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CarrierIdentityColumn
			{
				get
				{
					return this.columnCarrierIdentity;
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000173 RID: 371 RVA: 0x00006F80 File Offset: 0x00005180
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

			// Token: 0x17000068 RID: 104
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.LocalizedInfoRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.LocalizedInfoRow)base.Rows[index];
				}
			}

			// Token: 0x14000011 RID: 17
			// (add) Token: 0x06000175 RID: 373 RVA: 0x00006FA0 File Offset: 0x000051A0
			// (remove) Token: 0x06000176 RID: 374 RVA: 0x00006FD8 File Offset: 0x000051D8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.LocalizedInfoRowChangeEventHandler LocalizedInfoRowChanging;

			// Token: 0x14000012 RID: 18
			// (add) Token: 0x06000177 RID: 375 RVA: 0x00007010 File Offset: 0x00005210
			// (remove) Token: 0x06000178 RID: 376 RVA: 0x00007048 File Offset: 0x00005248
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.LocalizedInfoRowChangeEventHandler LocalizedInfoRowChanged;

			// Token: 0x14000013 RID: 19
			// (add) Token: 0x06000179 RID: 377 RVA: 0x00007080 File Offset: 0x00005280
			// (remove) Token: 0x0600017A RID: 378 RVA: 0x000070B8 File Offset: 0x000052B8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.LocalizedInfoRowChangeEventHandler LocalizedInfoRowDeleting;

			// Token: 0x14000014 RID: 20
			// (add) Token: 0x0600017B RID: 379 RVA: 0x000070F0 File Offset: 0x000052F0
			// (remove) Token: 0x0600017C RID: 380 RVA: 0x00007128 File Offset: 0x00005328
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.LocalizedInfoRowChangeEventHandler LocalizedInfoRowDeleted;

			// Token: 0x0600017D RID: 381 RVA: 0x0000715D File Offset: 0x0000535D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void AddLocalizedInfoRow(TextMessagingHostingData.LocalizedInfoRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x0600017E RID: 382 RVA: 0x0000716C File Offset: 0x0000536C
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

			// Token: 0x0600017F RID: 383 RVA: 0x000071B8 File Offset: 0x000053B8
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

			// Token: 0x06000180 RID: 384 RVA: 0x000071EC File Offset: 0x000053EC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				TextMessagingHostingData.LocalizedInfoDataTable localizedInfoDataTable = (TextMessagingHostingData.LocalizedInfoDataTable)base.Clone();
				localizedInfoDataTable.InitVars();
				return localizedInfoDataTable;
			}

			// Token: 0x06000181 RID: 385 RVA: 0x0000720C File Offset: 0x0000540C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.LocalizedInfoDataTable();
			}

			// Token: 0x06000182 RID: 386 RVA: 0x00007214 File Offset: 0x00005414
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal void InitVars()
			{
				this.columnDisplayName = base.Columns["DisplayName"];
				this.columnCulture = base.Columns["Culture"];
				this.columnCarrierIdentity = base.Columns["CarrierIdentity"];
			}

			// Token: 0x06000183 RID: 387 RVA: 0x00007264 File Offset: 0x00005464
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

			// Token: 0x06000184 RID: 388 RVA: 0x00007360 File Offset: 0x00005560
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.LocalizedInfoRow NewLocalizedInfoRow()
			{
				return (TextMessagingHostingData.LocalizedInfoRow)base.NewRow();
			}

			// Token: 0x06000185 RID: 389 RVA: 0x0000736D File Offset: 0x0000556D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.LocalizedInfoRow(builder);
			}

			// Token: 0x06000186 RID: 390 RVA: 0x00007375 File Offset: 0x00005575
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.LocalizedInfoRow);
			}

			// Token: 0x06000187 RID: 391 RVA: 0x00007381 File Offset: 0x00005581
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.LocalizedInfoRowChanged != null)
				{
					this.LocalizedInfoRowChanged(this, new TextMessagingHostingData.LocalizedInfoRowChangeEvent((TextMessagingHostingData.LocalizedInfoRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000188 RID: 392 RVA: 0x000073B4 File Offset: 0x000055B4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.LocalizedInfoRowChanging != null)
				{
					this.LocalizedInfoRowChanging(this, new TextMessagingHostingData.LocalizedInfoRowChangeEvent((TextMessagingHostingData.LocalizedInfoRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000189 RID: 393 RVA: 0x000073E7 File Offset: 0x000055E7
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

			// Token: 0x0600018A RID: 394 RVA: 0x0000741A File Offset: 0x0000561A
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

			// Token: 0x0600018B RID: 395 RVA: 0x0000744D File Offset: 0x0000564D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveLocalizedInfoRow(TextMessagingHostingData.LocalizedInfoRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x0600018C RID: 396 RVA: 0x0000745C File Offset: 0x0000565C
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

			// Token: 0x040000A0 RID: 160
			private DataColumn columnDisplayName;

			// Token: 0x040000A1 RID: 161
			private DataColumn columnCulture;

			// Token: 0x040000A2 RID: 162
			private DataColumn columnCarrierIdentity;
		}

		// Token: 0x02000022 RID: 34
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class ServicesDataTable : TypedTableBase<TextMessagingHostingData.ServicesRow>
		{
			// Token: 0x0600018D RID: 397 RVA: 0x00007654 File Offset: 0x00005854
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public ServicesDataTable()
			{
				base.TableName = "Services";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x0600018E RID: 398 RVA: 0x0000767C File Offset: 0x0000587C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

			// Token: 0x0600018F RID: 399 RVA: 0x00007724 File Offset: 0x00005924
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected ServicesDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000190 RID: 400 RVA: 0x00007734 File Offset: 0x00005934
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn Services_IdColumn
			{
				get
				{
					return this.columnServices_Id;
				}
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x06000191 RID: 401 RVA: 0x0000773C File Offset: 0x0000593C
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

			// Token: 0x1700006B RID: 107
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.ServicesRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.ServicesRow)base.Rows[index];
				}
			}

			// Token: 0x14000015 RID: 21
			// (add) Token: 0x06000193 RID: 403 RVA: 0x0000775C File Offset: 0x0000595C
			// (remove) Token: 0x06000194 RID: 404 RVA: 0x00007794 File Offset: 0x00005994
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServicesRowChangeEventHandler ServicesRowChanging;

			// Token: 0x14000016 RID: 22
			// (add) Token: 0x06000195 RID: 405 RVA: 0x000077CC File Offset: 0x000059CC
			// (remove) Token: 0x06000196 RID: 406 RVA: 0x00007804 File Offset: 0x00005A04
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServicesRowChangeEventHandler ServicesRowChanged;

			// Token: 0x14000017 RID: 23
			// (add) Token: 0x06000197 RID: 407 RVA: 0x0000783C File Offset: 0x00005A3C
			// (remove) Token: 0x06000198 RID: 408 RVA: 0x00007874 File Offset: 0x00005A74
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServicesRowChangeEventHandler ServicesRowDeleting;

			// Token: 0x14000018 RID: 24
			// (add) Token: 0x06000199 RID: 409 RVA: 0x000078AC File Offset: 0x00005AAC
			// (remove) Token: 0x0600019A RID: 410 RVA: 0x000078E4 File Offset: 0x00005AE4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServicesRowChangeEventHandler ServicesRowDeleted;

			// Token: 0x0600019B RID: 411 RVA: 0x00007919 File Offset: 0x00005B19
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void AddServicesRow(TextMessagingHostingData.ServicesRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x0600019C RID: 412 RVA: 0x00007928 File Offset: 0x00005B28
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.ServicesRow AddServicesRow()
			{
				TextMessagingHostingData.ServicesRow servicesRow = (TextMessagingHostingData.ServicesRow)base.NewRow();
				object[] array = new object[1];
				object[] itemArray = array;
				servicesRow.ItemArray = itemArray;
				base.Rows.Add(servicesRow);
				return servicesRow;
			}

			// Token: 0x0600019D RID: 413 RVA: 0x00007960 File Offset: 0x00005B60
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				TextMessagingHostingData.ServicesDataTable servicesDataTable = (TextMessagingHostingData.ServicesDataTable)base.Clone();
				servicesDataTable.InitVars();
				return servicesDataTable;
			}

			// Token: 0x0600019E RID: 414 RVA: 0x00007980 File Offset: 0x00005B80
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.ServicesDataTable();
			}

			// Token: 0x0600019F RID: 415 RVA: 0x00007987 File Offset: 0x00005B87
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnServices_Id = base.Columns["Services_Id"];
			}

			// Token: 0x060001A0 RID: 416 RVA: 0x000079A0 File Offset: 0x00005BA0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

			// Token: 0x060001A1 RID: 417 RVA: 0x00007A35 File Offset: 0x00005C35
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.ServicesRow NewServicesRow()
			{
				return (TextMessagingHostingData.ServicesRow)base.NewRow();
			}

			// Token: 0x060001A2 RID: 418 RVA: 0x00007A42 File Offset: 0x00005C42
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.ServicesRow(builder);
			}

			// Token: 0x060001A3 RID: 419 RVA: 0x00007A4A File Offset: 0x00005C4A
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.ServicesRow);
			}

			// Token: 0x060001A4 RID: 420 RVA: 0x00007A56 File Offset: 0x00005C56
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

			// Token: 0x060001A5 RID: 421 RVA: 0x00007A89 File Offset: 0x00005C89
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.ServicesRowChanging != null)
				{
					this.ServicesRowChanging(this, new TextMessagingHostingData.ServicesRowChangeEvent((TextMessagingHostingData.ServicesRow)e.Row, e.Action));
				}
			}

			// Token: 0x060001A6 RID: 422 RVA: 0x00007ABC File Offset: 0x00005CBC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.ServicesRowDeleted != null)
				{
					this.ServicesRowDeleted(this, new TextMessagingHostingData.ServicesRowChangeEvent((TextMessagingHostingData.ServicesRow)e.Row, e.Action));
				}
			}

			// Token: 0x060001A7 RID: 423 RVA: 0x00007AEF File Offset: 0x00005CEF
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

			// Token: 0x060001A8 RID: 424 RVA: 0x00007B22 File Offset: 0x00005D22
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveServicesRow(TextMessagingHostingData.ServicesRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x060001A9 RID: 425 RVA: 0x00007B30 File Offset: 0x00005D30
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

			// Token: 0x040000A7 RID: 167
			private DataColumn columnServices_Id;
		}

		// Token: 0x02000023 RID: 35
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class ServiceDataTable : TypedTableBase<TextMessagingHostingData.ServiceRow>
		{
			// Token: 0x060001AA RID: 426 RVA: 0x00007D28 File Offset: 0x00005F28
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public ServiceDataTable()
			{
				base.TableName = "Service";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x060001AB RID: 427 RVA: 0x00007D50 File Offset: 0x00005F50
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

			// Token: 0x060001AC RID: 428 RVA: 0x00007DF8 File Offset: 0x00005FF8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected ServiceDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x060001AD RID: 429 RVA: 0x00007E08 File Offset: 0x00006008
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn Services_IdColumn
			{
				get
				{
					return this.columnServices_Id;
				}
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x060001AE RID: 430 RVA: 0x00007E10 File Offset: 0x00006010
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn RegionIso2Column
			{
				get
				{
					return this.columnRegionIso2;
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x060001AF RID: 431 RVA: 0x00007E18 File Offset: 0x00006018
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn CarrierIdentityColumn
			{
				get
				{
					return this.columnCarrierIdentity;
				}
			}

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007E20 File Offset: 0x00006020
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn TypeColumn
			{
				get
				{
					return this.columnType;
				}
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x060001B1 RID: 433 RVA: 0x00007E28 File Offset: 0x00006028
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

			// Token: 0x17000071 RID: 113
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.ServiceRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.ServiceRow)base.Rows[index];
				}
			}

			// Token: 0x14000019 RID: 25
			// (add) Token: 0x060001B3 RID: 435 RVA: 0x00007E48 File Offset: 0x00006048
			// (remove) Token: 0x060001B4 RID: 436 RVA: 0x00007E80 File Offset: 0x00006080
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServiceRowChangeEventHandler ServiceRowChanging;

			// Token: 0x1400001A RID: 26
			// (add) Token: 0x060001B5 RID: 437 RVA: 0x00007EB8 File Offset: 0x000060B8
			// (remove) Token: 0x060001B6 RID: 438 RVA: 0x00007EF0 File Offset: 0x000060F0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServiceRowChangeEventHandler ServiceRowChanged;

			// Token: 0x1400001B RID: 27
			// (add) Token: 0x060001B7 RID: 439 RVA: 0x00007F28 File Offset: 0x00006128
			// (remove) Token: 0x060001B8 RID: 440 RVA: 0x00007F60 File Offset: 0x00006160
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServiceRowChangeEventHandler ServiceRowDeleting;

			// Token: 0x1400001C RID: 28
			// (add) Token: 0x060001B9 RID: 441 RVA: 0x00007F98 File Offset: 0x00006198
			// (remove) Token: 0x060001BA RID: 442 RVA: 0x00007FD0 File Offset: 0x000061D0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.ServiceRowChangeEventHandler ServiceRowDeleted;

			// Token: 0x060001BB RID: 443 RVA: 0x00008005 File Offset: 0x00006205
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void AddServiceRow(TextMessagingHostingData.ServiceRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x060001BC RID: 444 RVA: 0x00008014 File Offset: 0x00006214
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x060001BD RID: 445 RVA: 0x00008078 File Offset: 0x00006278
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.ServiceRow FindByRegionIso2CarrierIdentityType(string RegionIso2, int CarrierIdentity, string Type)
			{
				return (TextMessagingHostingData.ServiceRow)base.Rows.Find(new object[]
				{
					RegionIso2,
					CarrierIdentity,
					Type
				});
			}

			// Token: 0x060001BE RID: 446 RVA: 0x000080B0 File Offset: 0x000062B0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				TextMessagingHostingData.ServiceDataTable serviceDataTable = (TextMessagingHostingData.ServiceDataTable)base.Clone();
				serviceDataTable.InitVars();
				return serviceDataTable;
			}

			// Token: 0x060001BF RID: 447 RVA: 0x000080D0 File Offset: 0x000062D0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.ServiceDataTable();
			}

			// Token: 0x060001C0 RID: 448 RVA: 0x000080D8 File Offset: 0x000062D8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnServices_Id = base.Columns["Services_Id"];
				this.columnRegionIso2 = base.Columns["RegionIso2"];
				this.columnCarrierIdentity = base.Columns["CarrierIdentity"];
				this.columnType = base.Columns["Type"];
			}

			// Token: 0x060001C1 RID: 449 RVA: 0x00008140 File Offset: 0x00006340
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

			// Token: 0x060001C2 RID: 450 RVA: 0x0000826E File Offset: 0x0000646E
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.ServiceRow NewServiceRow()
			{
				return (TextMessagingHostingData.ServiceRow)base.NewRow();
			}

			// Token: 0x060001C3 RID: 451 RVA: 0x0000827B File Offset: 0x0000647B
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.ServiceRow(builder);
			}

			// Token: 0x060001C4 RID: 452 RVA: 0x00008283 File Offset: 0x00006483
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.ServiceRow);
			}

			// Token: 0x060001C5 RID: 453 RVA: 0x0000828F File Offset: 0x0000648F
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

			// Token: 0x060001C6 RID: 454 RVA: 0x000082C2 File Offset: 0x000064C2
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

			// Token: 0x060001C7 RID: 455 RVA: 0x000082F5 File Offset: 0x000064F5
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.ServiceRowDeleted != null)
				{
					this.ServiceRowDeleted(this, new TextMessagingHostingData.ServiceRowChangeEvent((TextMessagingHostingData.ServiceRow)e.Row, e.Action));
				}
			}

			// Token: 0x060001C8 RID: 456 RVA: 0x00008328 File Offset: 0x00006528
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.ServiceRowDeleting != null)
				{
					this.ServiceRowDeleting(this, new TextMessagingHostingData.ServiceRowChangeEvent((TextMessagingHostingData.ServiceRow)e.Row, e.Action));
				}
			}

			// Token: 0x060001C9 RID: 457 RVA: 0x0000835B File Offset: 0x0000655B
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveServiceRow(TextMessagingHostingData.ServiceRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x060001CA RID: 458 RVA: 0x0000836C File Offset: 0x0000656C
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

			// Token: 0x040000AC RID: 172
			private DataColumn columnServices_Id;

			// Token: 0x040000AD RID: 173
			private DataColumn columnRegionIso2;

			// Token: 0x040000AE RID: 174
			private DataColumn columnCarrierIdentity;

			// Token: 0x040000AF RID: 175
			private DataColumn columnType;
		}

		// Token: 0x02000024 RID: 36
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class VoiceCallForwardingDataTable : TypedTableBase<TextMessagingHostingData.VoiceCallForwardingRow>
		{
			// Token: 0x060001CB RID: 459 RVA: 0x00008564 File Offset: 0x00006764
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public VoiceCallForwardingDataTable()
			{
				base.TableName = "VoiceCallForwarding";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x060001CC RID: 460 RVA: 0x0000858C File Offset: 0x0000678C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x060001CD RID: 461 RVA: 0x00008634 File Offset: 0x00006834
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected VoiceCallForwardingDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x060001CE RID: 462 RVA: 0x00008644 File Offset: 0x00006844
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn EnableColumn
			{
				get
				{
					return this.columnEnable;
				}
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x060001CF RID: 463 RVA: 0x0000864C File Offset: 0x0000684C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn DisableColumn
			{
				get
				{
					return this.columnDisable;
				}
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x060001D0 RID: 464 RVA: 0x00008654 File Offset: 0x00006854
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn TypeColumn
			{
				get
				{
					return this.columnType;
				}
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000865C File Offset: 0x0000685C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn RegionIso2Column
			{
				get
				{
					return this.columnRegionIso2;
				}
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x060001D2 RID: 466 RVA: 0x00008664 File Offset: 0x00006864
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn CarrierIdentityColumn
			{
				get
				{
					return this.columnCarrierIdentity;
				}
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000866C File Offset: 0x0000686C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ServiceTypeColumn
			{
				get
				{
					return this.columnServiceType;
				}
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x060001D4 RID: 468 RVA: 0x00008674 File Offset: 0x00006874
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

			// Token: 0x17000079 RID: 121
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.VoiceCallForwardingRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.VoiceCallForwardingRow)base.Rows[index];
				}
			}

			// Token: 0x1400001D RID: 29
			// (add) Token: 0x060001D6 RID: 470 RVA: 0x00008694 File Offset: 0x00006894
			// (remove) Token: 0x060001D7 RID: 471 RVA: 0x000086CC File Offset: 0x000068CC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.VoiceCallForwardingRowChangeEventHandler VoiceCallForwardingRowChanging;

			// Token: 0x1400001E RID: 30
			// (add) Token: 0x060001D8 RID: 472 RVA: 0x00008704 File Offset: 0x00006904
			// (remove) Token: 0x060001D9 RID: 473 RVA: 0x0000873C File Offset: 0x0000693C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.VoiceCallForwardingRowChangeEventHandler VoiceCallForwardingRowChanged;

			// Token: 0x1400001F RID: 31
			// (add) Token: 0x060001DA RID: 474 RVA: 0x00008774 File Offset: 0x00006974
			// (remove) Token: 0x060001DB RID: 475 RVA: 0x000087AC File Offset: 0x000069AC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.VoiceCallForwardingRowChangeEventHandler VoiceCallForwardingRowDeleting;

			// Token: 0x14000020 RID: 32
			// (add) Token: 0x060001DC RID: 476 RVA: 0x000087E4 File Offset: 0x000069E4
			// (remove) Token: 0x060001DD RID: 477 RVA: 0x0000881C File Offset: 0x00006A1C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.VoiceCallForwardingRowChangeEventHandler VoiceCallForwardingRowDeleted;

			// Token: 0x060001DE RID: 478 RVA: 0x00008851 File Offset: 0x00006A51
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void AddVoiceCallForwardingRow(TextMessagingHostingData.VoiceCallForwardingRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x060001DF RID: 479 RVA: 0x00008860 File Offset: 0x00006A60
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x060001E0 RID: 480 RVA: 0x000088B8 File Offset: 0x00006AB8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				TextMessagingHostingData.VoiceCallForwardingDataTable voiceCallForwardingDataTable = (TextMessagingHostingData.VoiceCallForwardingDataTable)base.Clone();
				voiceCallForwardingDataTable.InitVars();
				return voiceCallForwardingDataTable;
			}

			// Token: 0x060001E1 RID: 481 RVA: 0x000088D8 File Offset: 0x00006AD8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.VoiceCallForwardingDataTable();
			}

			// Token: 0x060001E2 RID: 482 RVA: 0x000088E0 File Offset: 0x00006AE0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal void InitVars()
			{
				this.columnEnable = base.Columns["Enable"];
				this.columnDisable = base.Columns["Disable"];
				this.columnType = base.Columns["Type"];
				this.columnRegionIso2 = base.Columns["RegionIso2"];
				this.columnCarrierIdentity = base.Columns["CarrierIdentity"];
				this.columnServiceType = base.Columns["ServiceType"];
			}

			// Token: 0x060001E3 RID: 483 RVA: 0x00008974 File Offset: 0x00006B74
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

			// Token: 0x060001E4 RID: 484 RVA: 0x00008B2C File Offset: 0x00006D2C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.VoiceCallForwardingRow NewVoiceCallForwardingRow()
			{
				return (TextMessagingHostingData.VoiceCallForwardingRow)base.NewRow();
			}

			// Token: 0x060001E5 RID: 485 RVA: 0x00008B39 File Offset: 0x00006D39
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.VoiceCallForwardingRow(builder);
			}

			// Token: 0x060001E6 RID: 486 RVA: 0x00008B41 File Offset: 0x00006D41
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.VoiceCallForwardingRow);
			}

			// Token: 0x060001E7 RID: 487 RVA: 0x00008B4D File Offset: 0x00006D4D
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

			// Token: 0x060001E8 RID: 488 RVA: 0x00008B80 File Offset: 0x00006D80
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

			// Token: 0x060001E9 RID: 489 RVA: 0x00008BB3 File Offset: 0x00006DB3
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.VoiceCallForwardingRowDeleted != null)
				{
					this.VoiceCallForwardingRowDeleted(this, new TextMessagingHostingData.VoiceCallForwardingRowChangeEvent((TextMessagingHostingData.VoiceCallForwardingRow)e.Row, e.Action));
				}
			}

			// Token: 0x060001EA RID: 490 RVA: 0x00008BE6 File Offset: 0x00006DE6
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.VoiceCallForwardingRowDeleting != null)
				{
					this.VoiceCallForwardingRowDeleting(this, new TextMessagingHostingData.VoiceCallForwardingRowChangeEvent((TextMessagingHostingData.VoiceCallForwardingRow)e.Row, e.Action));
				}
			}

			// Token: 0x060001EB RID: 491 RVA: 0x00008C19 File Offset: 0x00006E19
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void RemoveVoiceCallForwardingRow(TextMessagingHostingData.VoiceCallForwardingRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x060001EC RID: 492 RVA: 0x00008C28 File Offset: 0x00006E28
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

			// Token: 0x040000B4 RID: 180
			private DataColumn columnEnable;

			// Token: 0x040000B5 RID: 181
			private DataColumn columnDisable;

			// Token: 0x040000B6 RID: 182
			private DataColumn columnType;

			// Token: 0x040000B7 RID: 183
			private DataColumn columnRegionIso2;

			// Token: 0x040000B8 RID: 184
			private DataColumn columnCarrierIdentity;

			// Token: 0x040000B9 RID: 185
			private DataColumn columnServiceType;
		}

		// Token: 0x02000025 RID: 37
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class SmtpToSmsGatewayDataTable : TypedTableBase<TextMessagingHostingData.SmtpToSmsGatewayRow>
		{
			// Token: 0x060001ED RID: 493 RVA: 0x00008E20 File Offset: 0x00007020
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public SmtpToSmsGatewayDataTable()
			{
				base.TableName = "SmtpToSmsGateway";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x060001EE RID: 494 RVA: 0x00008E48 File Offset: 0x00007048
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

			// Token: 0x060001EF RID: 495 RVA: 0x00008EF0 File Offset: 0x000070F0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected SmtpToSmsGatewayDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x060001F0 RID: 496 RVA: 0x00008F00 File Offset: 0x00007100
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn RegionIso2Column
			{
				get
				{
					return this.columnRegionIso2;
				}
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x060001F1 RID: 497 RVA: 0x00008F08 File Offset: 0x00007108
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CarrierIdentityColumn
			{
				get
				{
					return this.columnCarrierIdentity;
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060001F2 RID: 498 RVA: 0x00008F10 File Offset: 0x00007110
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ServiceTypeColumn
			{
				get
				{
					return this.columnServiceType;
				}
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x060001F3 RID: 499 RVA: 0x00008F18 File Offset: 0x00007118
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

			// Token: 0x1700007E RID: 126
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.SmtpToSmsGatewayRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.SmtpToSmsGatewayRow)base.Rows[index];
				}
			}

			// Token: 0x14000021 RID: 33
			// (add) Token: 0x060001F5 RID: 501 RVA: 0x00008F38 File Offset: 0x00007138
			// (remove) Token: 0x060001F6 RID: 502 RVA: 0x00008F70 File Offset: 0x00007170
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.SmtpToSmsGatewayRowChangeEventHandler SmtpToSmsGatewayRowChanging;

			// Token: 0x14000022 RID: 34
			// (add) Token: 0x060001F7 RID: 503 RVA: 0x00008FA8 File Offset: 0x000071A8
			// (remove) Token: 0x060001F8 RID: 504 RVA: 0x00008FE0 File Offset: 0x000071E0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.SmtpToSmsGatewayRowChangeEventHandler SmtpToSmsGatewayRowChanged;

			// Token: 0x14000023 RID: 35
			// (add) Token: 0x060001F9 RID: 505 RVA: 0x00009018 File Offset: 0x00007218
			// (remove) Token: 0x060001FA RID: 506 RVA: 0x00009050 File Offset: 0x00007250
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.SmtpToSmsGatewayRowChangeEventHandler SmtpToSmsGatewayRowDeleting;

			// Token: 0x14000024 RID: 36
			// (add) Token: 0x060001FB RID: 507 RVA: 0x00009088 File Offset: 0x00007288
			// (remove) Token: 0x060001FC RID: 508 RVA: 0x000090C0 File Offset: 0x000072C0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.SmtpToSmsGatewayRowChangeEventHandler SmtpToSmsGatewayRowDeleted;

			// Token: 0x060001FD RID: 509 RVA: 0x000090F5 File Offset: 0x000072F5
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void AddSmtpToSmsGatewayRow(TextMessagingHostingData.SmtpToSmsGatewayRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x060001FE RID: 510 RVA: 0x00009104 File Offset: 0x00007304
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

			// Token: 0x060001FF RID: 511 RVA: 0x0000914C File Offset: 0x0000734C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				TextMessagingHostingData.SmtpToSmsGatewayDataTable smtpToSmsGatewayDataTable = (TextMessagingHostingData.SmtpToSmsGatewayDataTable)base.Clone();
				smtpToSmsGatewayDataTable.InitVars();
				return smtpToSmsGatewayDataTable;
			}

			// Token: 0x06000200 RID: 512 RVA: 0x0000916C File Offset: 0x0000736C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.SmtpToSmsGatewayDataTable();
			}

			// Token: 0x06000201 RID: 513 RVA: 0x00009174 File Offset: 0x00007374
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnRegionIso2 = base.Columns["RegionIso2"];
				this.columnCarrierIdentity = base.Columns["CarrierIdentity"];
				this.columnServiceType = base.Columns["ServiceType"];
			}

			// Token: 0x06000202 RID: 514 RVA: 0x000091C4 File Offset: 0x000073C4
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

			// Token: 0x06000203 RID: 515 RVA: 0x000092E5 File Offset: 0x000074E5
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.SmtpToSmsGatewayRow NewSmtpToSmsGatewayRow()
			{
				return (TextMessagingHostingData.SmtpToSmsGatewayRow)base.NewRow();
			}

			// Token: 0x06000204 RID: 516 RVA: 0x000092F2 File Offset: 0x000074F2
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.SmtpToSmsGatewayRow(builder);
			}

			// Token: 0x06000205 RID: 517 RVA: 0x000092FA File Offset: 0x000074FA
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.SmtpToSmsGatewayRow);
			}

			// Token: 0x06000206 RID: 518 RVA: 0x00009306 File Offset: 0x00007506
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

			// Token: 0x06000207 RID: 519 RVA: 0x00009339 File Offset: 0x00007539
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.SmtpToSmsGatewayRowChanging != null)
				{
					this.SmtpToSmsGatewayRowChanging(this, new TextMessagingHostingData.SmtpToSmsGatewayRowChangeEvent((TextMessagingHostingData.SmtpToSmsGatewayRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000208 RID: 520 RVA: 0x0000936C File Offset: 0x0000756C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.SmtpToSmsGatewayRowDeleted != null)
				{
					this.SmtpToSmsGatewayRowDeleted(this, new TextMessagingHostingData.SmtpToSmsGatewayRowChangeEvent((TextMessagingHostingData.SmtpToSmsGatewayRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000209 RID: 521 RVA: 0x0000939F File Offset: 0x0000759F
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

			// Token: 0x0600020A RID: 522 RVA: 0x000093D2 File Offset: 0x000075D2
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void RemoveSmtpToSmsGatewayRow(TextMessagingHostingData.SmtpToSmsGatewayRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x0600020B RID: 523 RVA: 0x000093E0 File Offset: 0x000075E0
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

			// Token: 0x040000BE RID: 190
			private DataColumn columnRegionIso2;

			// Token: 0x040000BF RID: 191
			private DataColumn columnCarrierIdentity;

			// Token: 0x040000C0 RID: 192
			private DataColumn columnServiceType;
		}

		// Token: 0x02000026 RID: 38
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class RecipientAddressingDataTable : TypedTableBase<TextMessagingHostingData.RecipientAddressingRow>
		{
			// Token: 0x0600020C RID: 524 RVA: 0x000095D8 File Offset: 0x000077D8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public RecipientAddressingDataTable()
			{
				base.TableName = "RecipientAddressing";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x0600020D RID: 525 RVA: 0x00009600 File Offset: 0x00007800
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

			// Token: 0x0600020E RID: 526 RVA: 0x000096A8 File Offset: 0x000078A8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected RecipientAddressingDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x0600020F RID: 527 RVA: 0x000096B8 File Offset: 0x000078B8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn RegionIso2Column
			{
				get
				{
					return this.columnRegionIso2;
				}
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x06000210 RID: 528 RVA: 0x000096C0 File Offset: 0x000078C0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn SmtpAddressColumn
			{
				get
				{
					return this.columnSmtpAddress;
				}
			}

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x06000211 RID: 529 RVA: 0x000096C8 File Offset: 0x000078C8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn CarrierIdentityColumn
			{
				get
				{
					return this.columnCarrierIdentity;
				}
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x06000212 RID: 530 RVA: 0x000096D0 File Offset: 0x000078D0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ServiceTypeColumn
			{
				get
				{
					return this.columnServiceType;
				}
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x06000213 RID: 531 RVA: 0x000096D8 File Offset: 0x000078D8
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

			// Token: 0x17000084 RID: 132
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RecipientAddressingRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.RecipientAddressingRow)base.Rows[index];
				}
			}

			// Token: 0x14000025 RID: 37
			// (add) Token: 0x06000215 RID: 533 RVA: 0x000096F8 File Offset: 0x000078F8
			// (remove) Token: 0x06000216 RID: 534 RVA: 0x00009730 File Offset: 0x00007930
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RecipientAddressingRowChangeEventHandler RecipientAddressingRowChanging;

			// Token: 0x14000026 RID: 38
			// (add) Token: 0x06000217 RID: 535 RVA: 0x00009768 File Offset: 0x00007968
			// (remove) Token: 0x06000218 RID: 536 RVA: 0x000097A0 File Offset: 0x000079A0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RecipientAddressingRowChangeEventHandler RecipientAddressingRowChanged;

			// Token: 0x14000027 RID: 39
			// (add) Token: 0x06000219 RID: 537 RVA: 0x000097D8 File Offset: 0x000079D8
			// (remove) Token: 0x0600021A RID: 538 RVA: 0x00009810 File Offset: 0x00007A10
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RecipientAddressingRowChangeEventHandler RecipientAddressingRowDeleting;

			// Token: 0x14000028 RID: 40
			// (add) Token: 0x0600021B RID: 539 RVA: 0x00009848 File Offset: 0x00007A48
			// (remove) Token: 0x0600021C RID: 540 RVA: 0x00009880 File Offset: 0x00007A80
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.RecipientAddressingRowChangeEventHandler RecipientAddressingRowDeleted;

			// Token: 0x0600021D RID: 541 RVA: 0x000098B5 File Offset: 0x00007AB5
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void AddRecipientAddressingRow(TextMessagingHostingData.RecipientAddressingRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x0600021E RID: 542 RVA: 0x000098C4 File Offset: 0x00007AC4
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

			// Token: 0x0600021F RID: 543 RVA: 0x00009910 File Offset: 0x00007B10
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				TextMessagingHostingData.RecipientAddressingDataTable recipientAddressingDataTable = (TextMessagingHostingData.RecipientAddressingDataTable)base.Clone();
				recipientAddressingDataTable.InitVars();
				return recipientAddressingDataTable;
			}

			// Token: 0x06000220 RID: 544 RVA: 0x00009930 File Offset: 0x00007B30
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.RecipientAddressingDataTable();
			}

			// Token: 0x06000221 RID: 545 RVA: 0x00009938 File Offset: 0x00007B38
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				this.columnRegionIso2 = base.Columns["RegionIso2"];
				this.columnSmtpAddress = base.Columns["SmtpAddress"];
				this.columnCarrierIdentity = base.Columns["CarrierIdentity"];
				this.columnServiceType = base.Columns["ServiceType"];
			}

			// Token: 0x06000222 RID: 546 RVA: 0x000099A0 File Offset: 0x00007BA0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x06000223 RID: 547 RVA: 0x00009AFE File Offset: 0x00007CFE
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RecipientAddressingRow NewRecipientAddressingRow()
			{
				return (TextMessagingHostingData.RecipientAddressingRow)base.NewRow();
			}

			// Token: 0x06000224 RID: 548 RVA: 0x00009B0B File Offset: 0x00007D0B
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.RecipientAddressingRow(builder);
			}

			// Token: 0x06000225 RID: 549 RVA: 0x00009B13 File Offset: 0x00007D13
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.RecipientAddressingRow);
			}

			// Token: 0x06000226 RID: 550 RVA: 0x00009B1F File Offset: 0x00007D1F
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.RecipientAddressingRowChanged != null)
				{
					this.RecipientAddressingRowChanged(this, new TextMessagingHostingData.RecipientAddressingRowChangeEvent((TextMessagingHostingData.RecipientAddressingRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000227 RID: 551 RVA: 0x00009B52 File Offset: 0x00007D52
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

			// Token: 0x06000228 RID: 552 RVA: 0x00009B85 File Offset: 0x00007D85
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.RecipientAddressingRowDeleted != null)
				{
					this.RecipientAddressingRowDeleted(this, new TextMessagingHostingData.RecipientAddressingRowChangeEvent((TextMessagingHostingData.RecipientAddressingRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000229 RID: 553 RVA: 0x00009BB8 File Offset: 0x00007DB8
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

			// Token: 0x0600022A RID: 554 RVA: 0x00009BEB File Offset: 0x00007DEB
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void RemoveRecipientAddressingRow(TextMessagingHostingData.RecipientAddressingRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x0600022B RID: 555 RVA: 0x00009BFC File Offset: 0x00007DFC
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

			// Token: 0x040000C5 RID: 197
			private DataColumn columnRegionIso2;

			// Token: 0x040000C6 RID: 198
			private DataColumn columnSmtpAddress;

			// Token: 0x040000C7 RID: 199
			private DataColumn columnCarrierIdentity;

			// Token: 0x040000C8 RID: 200
			private DataColumn columnServiceType;
		}

		// Token: 0x02000027 RID: 39
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class MessageRenderingDataTable : TypedTableBase<TextMessagingHostingData.MessageRenderingRow>
		{
			// Token: 0x0600022C RID: 556 RVA: 0x00009DF4 File Offset: 0x00007FF4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public MessageRenderingDataTable()
			{
				base.TableName = "MessageRendering";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x0600022D RID: 557 RVA: 0x00009E1C File Offset: 0x0000801C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

			// Token: 0x0600022E RID: 558 RVA: 0x00009EC4 File Offset: 0x000080C4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected MessageRenderingDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x0600022F RID: 559 RVA: 0x00009ED4 File Offset: 0x000080D4
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ContainerColumn
			{
				get
				{
					return this.columnContainer;
				}
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x06000230 RID: 560 RVA: 0x00009EDC File Offset: 0x000080DC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn RegionIso2Column
			{
				get
				{
					return this.columnRegionIso2;
				}
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x06000231 RID: 561 RVA: 0x00009EE4 File Offset: 0x000080E4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn CarrierIdentityColumn
			{
				get
				{
					return this.columnCarrierIdentity;
				}
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x06000232 RID: 562 RVA: 0x00009EEC File Offset: 0x000080EC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn ServiceTypeColumn
			{
				get
				{
					return this.columnServiceType;
				}
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x06000233 RID: 563 RVA: 0x00009EF4 File Offset: 0x000080F4
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

			// Token: 0x1700008A RID: 138
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.MessageRenderingRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.MessageRenderingRow)base.Rows[index];
				}
			}

			// Token: 0x14000029 RID: 41
			// (add) Token: 0x06000235 RID: 565 RVA: 0x00009F14 File Offset: 0x00008114
			// (remove) Token: 0x06000236 RID: 566 RVA: 0x00009F4C File Offset: 0x0000814C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.MessageRenderingRowChangeEventHandler MessageRenderingRowChanging;

			// Token: 0x1400002A RID: 42
			// (add) Token: 0x06000237 RID: 567 RVA: 0x00009F84 File Offset: 0x00008184
			// (remove) Token: 0x06000238 RID: 568 RVA: 0x00009FBC File Offset: 0x000081BC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.MessageRenderingRowChangeEventHandler MessageRenderingRowChanged;

			// Token: 0x1400002B RID: 43
			// (add) Token: 0x06000239 RID: 569 RVA: 0x00009FF4 File Offset: 0x000081F4
			// (remove) Token: 0x0600023A RID: 570 RVA: 0x0000A02C File Offset: 0x0000822C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.MessageRenderingRowChangeEventHandler MessageRenderingRowDeleting;

			// Token: 0x1400002C RID: 44
			// (add) Token: 0x0600023B RID: 571 RVA: 0x0000A064 File Offset: 0x00008264
			// (remove) Token: 0x0600023C RID: 572 RVA: 0x0000A09C File Offset: 0x0000829C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.MessageRenderingRowChangeEventHandler MessageRenderingRowDeleted;

			// Token: 0x0600023D RID: 573 RVA: 0x0000A0D1 File Offset: 0x000082D1
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void AddMessageRenderingRow(TextMessagingHostingData.MessageRenderingRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x0600023E RID: 574 RVA: 0x0000A0E0 File Offset: 0x000082E0
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

			// Token: 0x0600023F RID: 575 RVA: 0x0000A12C File Offset: 0x0000832C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				TextMessagingHostingData.MessageRenderingDataTable messageRenderingDataTable = (TextMessagingHostingData.MessageRenderingDataTable)base.Clone();
				messageRenderingDataTable.InitVars();
				return messageRenderingDataTable;
			}

			// Token: 0x06000240 RID: 576 RVA: 0x0000A14C File Offset: 0x0000834C
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.MessageRenderingDataTable();
			}

			// Token: 0x06000241 RID: 577 RVA: 0x0000A154 File Offset: 0x00008354
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal void InitVars()
			{
				this.columnContainer = base.Columns["Container"];
				this.columnRegionIso2 = base.Columns["RegionIso2"];
				this.columnCarrierIdentity = base.Columns["CarrierIdentity"];
				this.columnServiceType = base.Columns["ServiceType"];
			}

			// Token: 0x06000242 RID: 578 RVA: 0x0000A1BC File Offset: 0x000083BC
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

			// Token: 0x06000243 RID: 579 RVA: 0x0000A31A File Offset: 0x0000851A
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.MessageRenderingRow NewMessageRenderingRow()
			{
				return (TextMessagingHostingData.MessageRenderingRow)base.NewRow();
			}

			// Token: 0x06000244 RID: 580 RVA: 0x0000A327 File Offset: 0x00008527
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.MessageRenderingRow(builder);
			}

			// Token: 0x06000245 RID: 581 RVA: 0x0000A32F File Offset: 0x0000852F
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.MessageRenderingRow);
			}

			// Token: 0x06000246 RID: 582 RVA: 0x0000A33B File Offset: 0x0000853B
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

			// Token: 0x06000247 RID: 583 RVA: 0x0000A36E File Offset: 0x0000856E
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.MessageRenderingRowChanging != null)
				{
					this.MessageRenderingRowChanging(this, new TextMessagingHostingData.MessageRenderingRowChangeEvent((TextMessagingHostingData.MessageRenderingRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000248 RID: 584 RVA: 0x0000A3A1 File Offset: 0x000085A1
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

			// Token: 0x06000249 RID: 585 RVA: 0x0000A3D4 File Offset: 0x000085D4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.MessageRenderingRowDeleting != null)
				{
					this.MessageRenderingRowDeleting(this, new TextMessagingHostingData.MessageRenderingRowChangeEvent((TextMessagingHostingData.MessageRenderingRow)e.Row, e.Action));
				}
			}

			// Token: 0x0600024A RID: 586 RVA: 0x0000A407 File Offset: 0x00008607
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveMessageRenderingRow(TextMessagingHostingData.MessageRenderingRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x0600024B RID: 587 RVA: 0x0000A418 File Offset: 0x00008618
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

			// Token: 0x040000CD RID: 205
			private DataColumn columnContainer;

			// Token: 0x040000CE RID: 206
			private DataColumn columnRegionIso2;

			// Token: 0x040000CF RID: 207
			private DataColumn columnCarrierIdentity;

			// Token: 0x040000D0 RID: 208
			private DataColumn columnServiceType;
		}

		// Token: 0x02000028 RID: 40
		[XmlSchemaProvider("GetTypedTableSchema")]
		[Serializable]
		public class CapacityDataTable : TypedTableBase<TextMessagingHostingData.CapacityRow>
		{
			// Token: 0x0600024C RID: 588 RVA: 0x0000A610 File Offset: 0x00008810
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public CapacityDataTable()
			{
				base.TableName = "Capacity";
				this.BeginInit();
				this.InitClass();
				this.EndInit();
			}

			// Token: 0x0600024D RID: 589 RVA: 0x0000A638 File Offset: 0x00008838
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

			// Token: 0x0600024E RID: 590 RVA: 0x0000A6E0 File Offset: 0x000088E0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected CapacityDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				this.InitVars();
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A6F0 File Offset: 0x000088F0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CodingSchemeColumn
			{
				get
				{
					return this.columnCodingScheme;
				}
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A6F8 File Offset: 0x000088F8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn Capacity_ValueColumn
			{
				get
				{
					return this.columnCapacity_Value;
				}
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A700 File Offset: 0x00008900
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn RegionIso2Column
			{
				get
				{
					return this.columnRegionIso2;
				}
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x06000252 RID: 594 RVA: 0x0000A708 File Offset: 0x00008908
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CarrierIdentityColumn
			{
				get
				{
					return this.columnCarrierIdentity;
				}
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x06000253 RID: 595 RVA: 0x0000A710 File Offset: 0x00008910
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ServiceTypeColumn
			{
				get
				{
					return this.columnServiceType;
				}
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x06000254 RID: 596 RVA: 0x0000A718 File Offset: 0x00008918
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

			// Token: 0x17000091 RID: 145
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.CapacityRow this[int index]
			{
				get
				{
					return (TextMessagingHostingData.CapacityRow)base.Rows[index];
				}
			}

			// Token: 0x1400002D RID: 45
			// (add) Token: 0x06000256 RID: 598 RVA: 0x0000A738 File Offset: 0x00008938
			// (remove) Token: 0x06000257 RID: 599 RVA: 0x0000A770 File Offset: 0x00008970
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CapacityRowChangeEventHandler CapacityRowChanging;

			// Token: 0x1400002E RID: 46
			// (add) Token: 0x06000258 RID: 600 RVA: 0x0000A7A8 File Offset: 0x000089A8
			// (remove) Token: 0x06000259 RID: 601 RVA: 0x0000A7E0 File Offset: 0x000089E0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CapacityRowChangeEventHandler CapacityRowChanged;

			// Token: 0x1400002F RID: 47
			// (add) Token: 0x0600025A RID: 602 RVA: 0x0000A818 File Offset: 0x00008A18
			// (remove) Token: 0x0600025B RID: 603 RVA: 0x0000A850 File Offset: 0x00008A50
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CapacityRowChangeEventHandler CapacityRowDeleting;

			// Token: 0x14000030 RID: 48
			// (add) Token: 0x0600025C RID: 604 RVA: 0x0000A888 File Offset: 0x00008A88
			// (remove) Token: 0x0600025D RID: 605 RVA: 0x0000A8C0 File Offset: 0x00008AC0
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event TextMessagingHostingData.CapacityRowChangeEventHandler CapacityRowDeleted;

			// Token: 0x0600025E RID: 606 RVA: 0x0000A8F5 File Offset: 0x00008AF5
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void AddCapacityRow(TextMessagingHostingData.CapacityRow row)
			{
				base.Rows.Add(row);
			}

			// Token: 0x0600025F RID: 607 RVA: 0x0000A904 File Offset: 0x00008B04
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

			// Token: 0x06000260 RID: 608 RVA: 0x0000A95C File Offset: 0x00008B5C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x06000261 RID: 609 RVA: 0x0000A998 File Offset: 0x00008B98
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				TextMessagingHostingData.CapacityDataTable capacityDataTable = (TextMessagingHostingData.CapacityDataTable)base.Clone();
				capacityDataTable.InitVars();
				return capacityDataTable;
			}

			// Token: 0x06000262 RID: 610 RVA: 0x0000A9B8 File Offset: 0x00008BB8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new TextMessagingHostingData.CapacityDataTable();
			}

			// Token: 0x06000263 RID: 611 RVA: 0x0000A9C0 File Offset: 0x00008BC0
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

			// Token: 0x06000264 RID: 612 RVA: 0x0000AA3C File Offset: 0x00008C3C
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

			// Token: 0x06000265 RID: 613 RVA: 0x0000ABDC File Offset: 0x00008DDC
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CapacityRow NewCapacityRow()
			{
				return (TextMessagingHostingData.CapacityRow)base.NewRow();
			}

			// Token: 0x06000266 RID: 614 RVA: 0x0000ABE9 File Offset: 0x00008DE9
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new TextMessagingHostingData.CapacityRow(builder);
			}

			// Token: 0x06000267 RID: 615 RVA: 0x0000ABF1 File Offset: 0x00008DF1
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(TextMessagingHostingData.CapacityRow);
			}

			// Token: 0x06000268 RID: 616 RVA: 0x0000ABFD File Offset: 0x00008DFD
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.CapacityRowChanged != null)
				{
					this.CapacityRowChanged(this, new TextMessagingHostingData.CapacityRowChangeEvent((TextMessagingHostingData.CapacityRow)e.Row, e.Action));
				}
			}

			// Token: 0x06000269 RID: 617 RVA: 0x0000AC30 File Offset: 0x00008E30
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

			// Token: 0x0600026A RID: 618 RVA: 0x0000AC63 File Offset: 0x00008E63
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

			// Token: 0x0600026B RID: 619 RVA: 0x0000AC96 File Offset: 0x00008E96
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

			// Token: 0x0600026C RID: 620 RVA: 0x0000ACC9 File Offset: 0x00008EC9
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemoveCapacityRow(TextMessagingHostingData.CapacityRow row)
			{
				base.Rows.Remove(row);
			}

			// Token: 0x0600026D RID: 621 RVA: 0x0000ACD8 File Offset: 0x00008ED8
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

			// Token: 0x040000D5 RID: 213
			private DataColumn columnCodingScheme;

			// Token: 0x040000D6 RID: 214
			private DataColumn columnCapacity_Value;

			// Token: 0x040000D7 RID: 215
			private DataColumn columnRegionIso2;

			// Token: 0x040000D8 RID: 216
			private DataColumn columnCarrierIdentity;

			// Token: 0x040000D9 RID: 217
			private DataColumn columnServiceType;
		}

		// Token: 0x02000029 RID: 41
		public class RegionsRow : DataRow
		{
			// Token: 0x0600026E RID: 622 RVA: 0x0000AED0 File Offset: 0x000090D0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal RegionsRow(DataRowBuilder rb) : base(rb)
			{
				this.tableRegions = (TextMessagingHostingData.RegionsDataTable)base.Table;
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x0600026F RID: 623 RVA: 0x0000AEEA File Offset: 0x000090EA
			// (set) Token: 0x06000270 RID: 624 RVA: 0x0000AF02 File Offset: 0x00009102
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

			// Token: 0x06000271 RID: 625 RVA: 0x0000AF1B File Offset: 0x0000911B
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RegionRow[] GetRegionRows()
			{
				if (base.Table.ChildRelations["Regions_Region"] == null)
				{
					return new TextMessagingHostingData.RegionRow[0];
				}
				return (TextMessagingHostingData.RegionRow[])base.GetChildRows(base.Table.ChildRelations["Regions_Region"]);
			}

			// Token: 0x040000DE RID: 222
			private TextMessagingHostingData.RegionsDataTable tableRegions;
		}

		// Token: 0x0200002A RID: 42
		public class RegionRow : DataRow
		{
			// Token: 0x06000272 RID: 626 RVA: 0x0000AF5B File Offset: 0x0000915B
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal RegionRow(DataRowBuilder rb) : base(rb)
			{
				this.tableRegion = (TextMessagingHostingData.RegionDataTable)base.Table;
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x06000273 RID: 627 RVA: 0x0000AF75 File Offset: 0x00009175
			// (set) Token: 0x06000274 RID: 628 RVA: 0x0000AF8D File Offset: 0x0000918D
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

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x06000275 RID: 629 RVA: 0x0000AFA4 File Offset: 0x000091A4
			// (set) Token: 0x06000276 RID: 630 RVA: 0x0000AFE8 File Offset: 0x000091E8
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

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x06000277 RID: 631 RVA: 0x0000AFFC File Offset: 0x000091FC
			// (set) Token: 0x06000278 RID: 632 RVA: 0x0000B014 File Offset: 0x00009214
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x06000279 RID: 633 RVA: 0x0000B028 File Offset: 0x00009228
			// (set) Token: 0x0600027A RID: 634 RVA: 0x0000B06C File Offset: 0x0000926C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x0600027B RID: 635 RVA: 0x0000B085 File Offset: 0x00009285
			// (set) Token: 0x0600027C RID: 636 RVA: 0x0000B0A7 File Offset: 0x000092A7
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x0600027D RID: 637 RVA: 0x0000B0C5 File Offset: 0x000092C5
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsPhoneNumberExampleNull()
			{
				return base.IsNull(this.tableRegion.PhoneNumberExampleColumn);
			}

			// Token: 0x0600027E RID: 638 RVA: 0x0000B0D8 File Offset: 0x000092D8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetPhoneNumberExampleNull()
			{
				base[this.tableRegion.PhoneNumberExampleColumn] = Convert.DBNull;
			}

			// Token: 0x0600027F RID: 639 RVA: 0x0000B0F0 File Offset: 0x000092F0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsRegions_IdNull()
			{
				return base.IsNull(this.tableRegion.Regions_IdColumn);
			}

			// Token: 0x06000280 RID: 640 RVA: 0x0000B103 File Offset: 0x00009303
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetRegions_IdNull()
			{
				base[this.tableRegion.Regions_IdColumn] = Convert.DBNull;
			}

			// Token: 0x06000281 RID: 641 RVA: 0x0000B11B File Offset: 0x0000931B
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.ServiceRow[] GetServiceRows()
			{
				if (base.Table.ChildRelations["FK_Region_Service"] == null)
				{
					return new TextMessagingHostingData.ServiceRow[0];
				}
				return (TextMessagingHostingData.ServiceRow[])base.GetChildRows(base.Table.ChildRelations["FK_Region_Service"]);
			}

			// Token: 0x040000DF RID: 223
			private TextMessagingHostingData.RegionDataTable tableRegion;
		}

		// Token: 0x0200002B RID: 43
		public class CarriersRow : DataRow
		{
			// Token: 0x06000282 RID: 642 RVA: 0x0000B15B File Offset: 0x0000935B
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal CarriersRow(DataRowBuilder rb) : base(rb)
			{
				this.tableCarriers = (TextMessagingHostingData.CarriersDataTable)base.Table;
			}

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x06000283 RID: 643 RVA: 0x0000B175 File Offset: 0x00009375
			// (set) Token: 0x06000284 RID: 644 RVA: 0x0000B18D File Offset: 0x0000938D
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

			// Token: 0x06000285 RID: 645 RVA: 0x0000B1A6 File Offset: 0x000093A6
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

			// Token: 0x040000E0 RID: 224
			private TextMessagingHostingData.CarriersDataTable tableCarriers;
		}

		// Token: 0x0200002C RID: 44
		public class CarrierRow : DataRow
		{
			// Token: 0x06000286 RID: 646 RVA: 0x0000B1E6 File Offset: 0x000093E6
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal CarrierRow(DataRowBuilder rb) : base(rb)
			{
				this.tableCarrier = (TextMessagingHostingData.CarrierDataTable)base.Table;
			}

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x06000287 RID: 647 RVA: 0x0000B200 File Offset: 0x00009400
			// (set) Token: 0x06000288 RID: 648 RVA: 0x0000B218 File Offset: 0x00009418
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

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x06000289 RID: 649 RVA: 0x0000B234 File Offset: 0x00009434
			// (set) Token: 0x0600028A RID: 650 RVA: 0x0000B278 File Offset: 0x00009478
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x0600028B RID: 651 RVA: 0x0000B291 File Offset: 0x00009491
			// (set) Token: 0x0600028C RID: 652 RVA: 0x0000B2B3 File Offset: 0x000094B3
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

			// Token: 0x0600028D RID: 653 RVA: 0x0000B2D1 File Offset: 0x000094D1
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsCarriers_IdNull()
			{
				return base.IsNull(this.tableCarrier.Carriers_IdColumn);
			}

			// Token: 0x0600028E RID: 654 RVA: 0x0000B2E4 File Offset: 0x000094E4
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetCarriers_IdNull()
			{
				base[this.tableCarrier.Carriers_IdColumn] = Convert.DBNull;
			}

			// Token: 0x0600028F RID: 655 RVA: 0x0000B2FC File Offset: 0x000094FC
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

			// Token: 0x06000290 RID: 656 RVA: 0x0000B33C File Offset: 0x0000953C
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

			// Token: 0x040000E1 RID: 225
			private TextMessagingHostingData.CarrierDataTable tableCarrier;
		}

		// Token: 0x0200002D RID: 45
		public class LocalizedInfoRow : DataRow
		{
			// Token: 0x06000291 RID: 657 RVA: 0x0000B37C File Offset: 0x0000957C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal LocalizedInfoRow(DataRowBuilder rb) : base(rb)
			{
				this.tableLocalizedInfo = (TextMessagingHostingData.LocalizedInfoDataTable)base.Table;
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x06000292 RID: 658 RVA: 0x0000B398 File Offset: 0x00009598
			// (set) Token: 0x06000293 RID: 659 RVA: 0x0000B3DC File Offset: 0x000095DC
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

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x06000294 RID: 660 RVA: 0x0000B3F0 File Offset: 0x000095F0
			// (set) Token: 0x06000295 RID: 661 RVA: 0x0000B408 File Offset: 0x00009608
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

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x06000296 RID: 662 RVA: 0x0000B41C File Offset: 0x0000961C
			// (set) Token: 0x06000297 RID: 663 RVA: 0x0000B434 File Offset: 0x00009634
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

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x06000298 RID: 664 RVA: 0x0000B44D File Offset: 0x0000964D
			// (set) Token: 0x06000299 RID: 665 RVA: 0x0000B46F File Offset: 0x0000966F
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

			// Token: 0x0600029A RID: 666 RVA: 0x0000B48D File Offset: 0x0000968D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsDisplayNameNull()
			{
				return base.IsNull(this.tableLocalizedInfo.DisplayNameColumn);
			}

			// Token: 0x0600029B RID: 667 RVA: 0x0000B4A0 File Offset: 0x000096A0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetDisplayNameNull()
			{
				base[this.tableLocalizedInfo.DisplayNameColumn] = Convert.DBNull;
			}

			// Token: 0x040000E2 RID: 226
			private TextMessagingHostingData.LocalizedInfoDataTable tableLocalizedInfo;
		}

		// Token: 0x0200002E RID: 46
		public class ServicesRow : DataRow
		{
			// Token: 0x0600029C RID: 668 RVA: 0x0000B4B8 File Offset: 0x000096B8
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal ServicesRow(DataRowBuilder rb) : base(rb)
			{
				this.tableServices = (TextMessagingHostingData.ServicesDataTable)base.Table;
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x0600029D RID: 669 RVA: 0x0000B4D2 File Offset: 0x000096D2
			// (set) Token: 0x0600029E RID: 670 RVA: 0x0000B4EA File Offset: 0x000096EA
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

			// Token: 0x0600029F RID: 671 RVA: 0x0000B503 File Offset: 0x00009703
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

			// Token: 0x040000E3 RID: 227
			private TextMessagingHostingData.ServicesDataTable tableServices;
		}

		// Token: 0x0200002F RID: 47
		public class ServiceRow : DataRow
		{
			// Token: 0x060002A0 RID: 672 RVA: 0x0000B543 File Offset: 0x00009743
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal ServiceRow(DataRowBuilder rb) : base(rb)
			{
				this.tableService = (TextMessagingHostingData.ServiceDataTable)base.Table;
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000B560 File Offset: 0x00009760
			// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000B5A4 File Offset: 0x000097A4
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

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000B5BD File Offset: 0x000097BD
			// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000B5D5 File Offset: 0x000097D5
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000B5E9 File Offset: 0x000097E9
			// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000B601 File Offset: 0x00009801
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000B61A File Offset: 0x0000981A
			// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000B632 File Offset: 0x00009832
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

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000B646 File Offset: 0x00009846
			// (set) Token: 0x060002AA RID: 682 RVA: 0x0000B668 File Offset: 0x00009868
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060002AB RID: 683 RVA: 0x0000B686 File Offset: 0x00009886
			// (set) Token: 0x060002AC RID: 684 RVA: 0x0000B6A8 File Offset: 0x000098A8
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

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B6C6 File Offset: 0x000098C6
			// (set) Token: 0x060002AE RID: 686 RVA: 0x0000B6E8 File Offset: 0x000098E8
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

			// Token: 0x060002AF RID: 687 RVA: 0x0000B706 File Offset: 0x00009906
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsServices_IdNull()
			{
				return base.IsNull(this.tableService.Services_IdColumn);
			}

			// Token: 0x060002B0 RID: 688 RVA: 0x0000B719 File Offset: 0x00009919
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void SetServices_IdNull()
			{
				base[this.tableService.Services_IdColumn] = Convert.DBNull;
			}

			// Token: 0x060002B1 RID: 689 RVA: 0x0000B731 File Offset: 0x00009931
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.VoiceCallForwardingRow[] GetVoiceCallForwardingRows()
			{
				if (base.Table.ChildRelations["FK_Service_VoiceCallForwarding"] == null)
				{
					return new TextMessagingHostingData.VoiceCallForwardingRow[0];
				}
				return (TextMessagingHostingData.VoiceCallForwardingRow[])base.GetChildRows(base.Table.ChildRelations["FK_Service_VoiceCallForwarding"]);
			}

			// Token: 0x060002B2 RID: 690 RVA: 0x0000B771 File Offset: 0x00009971
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.SmtpToSmsGatewayRow[] GetSmtpToSmsGatewayRows()
			{
				if (base.Table.ChildRelations["FK_Service_SmtpToSmsGateway"] == null)
				{
					return new TextMessagingHostingData.SmtpToSmsGatewayRow[0];
				}
				return (TextMessagingHostingData.SmtpToSmsGatewayRow[])base.GetChildRows(base.Table.ChildRelations["FK_Service_SmtpToSmsGateway"]);
			}

			// Token: 0x040000E4 RID: 228
			private TextMessagingHostingData.ServiceDataTable tableService;
		}

		// Token: 0x02000030 RID: 48
		public class VoiceCallForwardingRow : DataRow
		{
			// Token: 0x060002B3 RID: 691 RVA: 0x0000B7B1 File Offset: 0x000099B1
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal VoiceCallForwardingRow(DataRowBuilder rb) : base(rb)
			{
				this.tableVoiceCallForwarding = (TextMessagingHostingData.VoiceCallForwardingDataTable)base.Table;
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000B7CC File Offset: 0x000099CC
			// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000B810 File Offset: 0x00009A10
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000B824 File Offset: 0x00009A24
			// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000B868 File Offset: 0x00009A68
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

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000B87C File Offset: 0x00009A7C
			// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000B8C0 File Offset: 0x00009AC0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060002BA RID: 698 RVA: 0x0000B8D4 File Offset: 0x00009AD4
			// (set) Token: 0x060002BB RID: 699 RVA: 0x0000B8EC File Offset: 0x00009AEC
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060002BC RID: 700 RVA: 0x0000B900 File Offset: 0x00009B00
			// (set) Token: 0x060002BD RID: 701 RVA: 0x0000B918 File Offset: 0x00009B18
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060002BE RID: 702 RVA: 0x0000B931 File Offset: 0x00009B31
			// (set) Token: 0x060002BF RID: 703 RVA: 0x0000B949 File Offset: 0x00009B49
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000B95D File Offset: 0x00009B5D
			// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000B97F File Offset: 0x00009B7F
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x060002C2 RID: 706 RVA: 0x0000B99D File Offset: 0x00009B9D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsEnableNull()
			{
				return base.IsNull(this.tableVoiceCallForwarding.EnableColumn);
			}

			// Token: 0x060002C3 RID: 707 RVA: 0x0000B9B0 File Offset: 0x00009BB0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetEnableNull()
			{
				base[this.tableVoiceCallForwarding.EnableColumn] = Convert.DBNull;
			}

			// Token: 0x060002C4 RID: 708 RVA: 0x0000B9C8 File Offset: 0x00009BC8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsDisableNull()
			{
				return base.IsNull(this.tableVoiceCallForwarding.DisableColumn);
			}

			// Token: 0x060002C5 RID: 709 RVA: 0x0000B9DB File Offset: 0x00009BDB
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetDisableNull()
			{
				base[this.tableVoiceCallForwarding.DisableColumn] = Convert.DBNull;
			}

			// Token: 0x060002C6 RID: 710 RVA: 0x0000B9F3 File Offset: 0x00009BF3
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsTypeNull()
			{
				return base.IsNull(this.tableVoiceCallForwarding.TypeColumn);
			}

			// Token: 0x060002C7 RID: 711 RVA: 0x0000BA06 File Offset: 0x00009C06
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetTypeNull()
			{
				base[this.tableVoiceCallForwarding.TypeColumn] = Convert.DBNull;
			}

			// Token: 0x040000E5 RID: 229
			private TextMessagingHostingData.VoiceCallForwardingDataTable tableVoiceCallForwarding;
		}

		// Token: 0x02000031 RID: 49
		public class SmtpToSmsGatewayRow : DataRow
		{
			// Token: 0x060002C8 RID: 712 RVA: 0x0000BA1E File Offset: 0x00009C1E
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal SmtpToSmsGatewayRow(DataRowBuilder rb) : base(rb)
			{
				this.tableSmtpToSmsGateway = (TextMessagingHostingData.SmtpToSmsGatewayDataTable)base.Table;
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000BA38 File Offset: 0x00009C38
			// (set) Token: 0x060002CA RID: 714 RVA: 0x0000BA50 File Offset: 0x00009C50
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x060002CB RID: 715 RVA: 0x0000BA64 File Offset: 0x00009C64
			// (set) Token: 0x060002CC RID: 716 RVA: 0x0000BA7C File Offset: 0x00009C7C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x060002CD RID: 717 RVA: 0x0000BA95 File Offset: 0x00009C95
			// (set) Token: 0x060002CE RID: 718 RVA: 0x0000BAAD File Offset: 0x00009CAD
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x060002CF RID: 719 RVA: 0x0000BAC1 File Offset: 0x00009CC1
			// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000BAE3 File Offset: 0x00009CE3
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x060002D1 RID: 721 RVA: 0x0000BB01 File Offset: 0x00009D01
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RecipientAddressingRow[] GetRecipientAddressingRows()
			{
				if (base.Table.ChildRelations["FK_SmtpToSmsGateway_RecipientAddressing"] == null)
				{
					return new TextMessagingHostingData.RecipientAddressingRow[0];
				}
				return (TextMessagingHostingData.RecipientAddressingRow[])base.GetChildRows(base.Table.ChildRelations["FK_SmtpToSmsGateway_RecipientAddressing"]);
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x0000BB41 File Offset: 0x00009D41
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

			// Token: 0x040000E6 RID: 230
			private TextMessagingHostingData.SmtpToSmsGatewayDataTable tableSmtpToSmsGateway;
		}

		// Token: 0x02000032 RID: 50
		public class RecipientAddressingRow : DataRow
		{
			// Token: 0x060002D3 RID: 723 RVA: 0x0000BB81 File Offset: 0x00009D81
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal RecipientAddressingRow(DataRowBuilder rb) : base(rb)
			{
				this.tableRecipientAddressing = (TextMessagingHostingData.RecipientAddressingDataTable)base.Table;
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000BB9B File Offset: 0x00009D9B
			// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000BBB3 File Offset: 0x00009DB3
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

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000BBC8 File Offset: 0x00009DC8
			// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000BC0C File Offset: 0x00009E0C
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

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000BC20 File Offset: 0x00009E20
			// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000BC38 File Offset: 0x00009E38
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

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x060002DA RID: 730 RVA: 0x0000BC51 File Offset: 0x00009E51
			// (set) Token: 0x060002DB RID: 731 RVA: 0x0000BC69 File Offset: 0x00009E69
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x060002DC RID: 732 RVA: 0x0000BC7D File Offset: 0x00009E7D
			// (set) Token: 0x060002DD RID: 733 RVA: 0x0000BC9F File Offset: 0x00009E9F
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

			// Token: 0x060002DE RID: 734 RVA: 0x0000BCBD File Offset: 0x00009EBD
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsSmtpAddressNull()
			{
				return base.IsNull(this.tableRecipientAddressing.SmtpAddressColumn);
			}

			// Token: 0x060002DF RID: 735 RVA: 0x0000BCD0 File Offset: 0x00009ED0
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetSmtpAddressNull()
			{
				base[this.tableRecipientAddressing.SmtpAddressColumn] = Convert.DBNull;
			}

			// Token: 0x040000E7 RID: 231
			private TextMessagingHostingData.RecipientAddressingDataTable tableRecipientAddressing;
		}

		// Token: 0x02000033 RID: 51
		public class MessageRenderingRow : DataRow
		{
			// Token: 0x060002E0 RID: 736 RVA: 0x0000BCE8 File Offset: 0x00009EE8
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal MessageRenderingRow(DataRowBuilder rb) : base(rb)
			{
				this.tableMessageRendering = (TextMessagingHostingData.MessageRenderingDataTable)base.Table;
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000BD04 File Offset: 0x00009F04
			// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000BD48 File Offset: 0x00009F48
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

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000BD5C File Offset: 0x00009F5C
			// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000BD74 File Offset: 0x00009F74
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

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000BD88 File Offset: 0x00009F88
			// (set) Token: 0x060002E6 RID: 742 RVA: 0x0000BDA0 File Offset: 0x00009FA0
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

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000BDB9 File Offset: 0x00009FB9
			// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000BDD1 File Offset: 0x00009FD1
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

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000BDE5 File Offset: 0x00009FE5
			// (set) Token: 0x060002EA RID: 746 RVA: 0x0000BE07 File Offset: 0x0000A007
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x060002EB RID: 747 RVA: 0x0000BE25 File Offset: 0x0000A025
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsContainerNull()
			{
				return base.IsNull(this.tableMessageRendering.ContainerColumn);
			}

			// Token: 0x060002EC RID: 748 RVA: 0x0000BE38 File Offset: 0x0000A038
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetContainerNull()
			{
				base[this.tableMessageRendering.ContainerColumn] = Convert.DBNull;
			}

			// Token: 0x060002ED RID: 749 RVA: 0x0000BE50 File Offset: 0x0000A050
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CapacityRow[] GetCapacityRows()
			{
				if (base.Table.ChildRelations["FK_MessageRendering_Capacity"] == null)
				{
					return new TextMessagingHostingData.CapacityRow[0];
				}
				return (TextMessagingHostingData.CapacityRow[])base.GetChildRows(base.Table.ChildRelations["FK_MessageRendering_Capacity"]);
			}

			// Token: 0x040000E8 RID: 232
			private TextMessagingHostingData.MessageRenderingDataTable tableMessageRendering;
		}

		// Token: 0x02000034 RID: 52
		public class CapacityRow : DataRow
		{
			// Token: 0x060002EE RID: 750 RVA: 0x0000BE90 File Offset: 0x0000A090
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal CapacityRow(DataRowBuilder rb) : base(rb)
			{
				this.tableCapacity = (TextMessagingHostingData.CapacityDataTable)base.Table;
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x060002EF RID: 751 RVA: 0x0000BEAA File Offset: 0x0000A0AA
			// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000BEC2 File Offset: 0x0000A0C2
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000BED8 File Offset: 0x0000A0D8
			// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000BF1C File Offset: 0x0000A11C
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
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

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000BF35 File Offset: 0x0000A135
			// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000BF4D File Offset: 0x0000A14D
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

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000BF61 File Offset: 0x0000A161
			// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000BF79 File Offset: 0x0000A179
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

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000BF92 File Offset: 0x0000A192
			// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000BFAA File Offset: 0x0000A1AA
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

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000BFBE File Offset: 0x0000A1BE
			// (set) Token: 0x060002FA RID: 762 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
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

			// Token: 0x060002FB RID: 763 RVA: 0x0000BFFE File Offset: 0x0000A1FE
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsCapacity_ValueNull()
			{
				return base.IsNull(this.tableCapacity.Capacity_ValueColumn);
			}

			// Token: 0x060002FC RID: 764 RVA: 0x0000C011 File Offset: 0x0000A211
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetCapacity_ValueNull()
			{
				base[this.tableCapacity.Capacity_ValueColumn] = Convert.DBNull;
			}

			// Token: 0x040000E9 RID: 233
			private TextMessagingHostingData.CapacityDataTable tableCapacity;
		}

		// Token: 0x02000035 RID: 53
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class RegionsRowChangeEvent : EventArgs
		{
			// Token: 0x060002FD RID: 765 RVA: 0x0000C029 File Offset: 0x0000A229
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public RegionsRowChangeEvent(TextMessagingHostingData.RegionsRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x060002FE RID: 766 RVA: 0x0000C03F File Offset: 0x0000A23F
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.RegionsRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x060002FF RID: 767 RVA: 0x0000C047 File Offset: 0x0000A247
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x040000EA RID: 234
			private TextMessagingHostingData.RegionsRow eventRow;

			// Token: 0x040000EB RID: 235
			private DataRowAction eventAction;
		}

		// Token: 0x02000036 RID: 54
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class RegionRowChangeEvent : EventArgs
		{
			// Token: 0x06000300 RID: 768 RVA: 0x0000C04F File Offset: 0x0000A24F
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public RegionRowChangeEvent(TextMessagingHostingData.RegionRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000301 RID: 769 RVA: 0x0000C065 File Offset: 0x0000A265
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RegionRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000302 RID: 770 RVA: 0x0000C06D File Offset: 0x0000A26D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x040000EC RID: 236
			private TextMessagingHostingData.RegionRow eventRow;

			// Token: 0x040000ED RID: 237
			private DataRowAction eventAction;
		}

		// Token: 0x02000037 RID: 55
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class CarriersRowChangeEvent : EventArgs
		{
			// Token: 0x06000303 RID: 771 RVA: 0x0000C075 File Offset: 0x0000A275
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public CarriersRowChangeEvent(TextMessagingHostingData.CarriersRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000304 RID: 772 RVA: 0x0000C08B File Offset: 0x0000A28B
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CarriersRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000305 RID: 773 RVA: 0x0000C093 File Offset: 0x0000A293
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x040000EE RID: 238
			private TextMessagingHostingData.CarriersRow eventRow;

			// Token: 0x040000EF RID: 239
			private DataRowAction eventAction;
		}

		// Token: 0x02000038 RID: 56
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class CarrierRowChangeEvent : EventArgs
		{
			// Token: 0x06000306 RID: 774 RVA: 0x0000C09B File Offset: 0x0000A29B
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public CarrierRowChangeEvent(TextMessagingHostingData.CarrierRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x06000307 RID: 775 RVA: 0x0000C0B1 File Offset: 0x0000A2B1
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.CarrierRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x06000308 RID: 776 RVA: 0x0000C0B9 File Offset: 0x0000A2B9
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x040000F0 RID: 240
			private TextMessagingHostingData.CarrierRow eventRow;

			// Token: 0x040000F1 RID: 241
			private DataRowAction eventAction;
		}

		// Token: 0x02000039 RID: 57
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class LocalizedInfoRowChangeEvent : EventArgs
		{
			// Token: 0x06000309 RID: 777 RVA: 0x0000C0C1 File Offset: 0x0000A2C1
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public LocalizedInfoRowChangeEvent(TextMessagingHostingData.LocalizedInfoRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x0600030A RID: 778 RVA: 0x0000C0D7 File Offset: 0x0000A2D7
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.LocalizedInfoRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x0600030B RID: 779 RVA: 0x0000C0DF File Offset: 0x0000A2DF
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x040000F2 RID: 242
			private TextMessagingHostingData.LocalizedInfoRow eventRow;

			// Token: 0x040000F3 RID: 243
			private DataRowAction eventAction;
		}

		// Token: 0x0200003A RID: 58
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class ServicesRowChangeEvent : EventArgs
		{
			// Token: 0x0600030C RID: 780 RVA: 0x0000C0E7 File Offset: 0x0000A2E7
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public ServicesRowChangeEvent(TextMessagingHostingData.ServicesRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x0600030D RID: 781 RVA: 0x0000C0FD File Offset: 0x0000A2FD
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.ServicesRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x0600030E RID: 782 RVA: 0x0000C105 File Offset: 0x0000A305
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x040000F4 RID: 244
			private TextMessagingHostingData.ServicesRow eventRow;

			// Token: 0x040000F5 RID: 245
			private DataRowAction eventAction;
		}

		// Token: 0x0200003B RID: 59
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class ServiceRowChangeEvent : EventArgs
		{
			// Token: 0x0600030F RID: 783 RVA: 0x0000C10D File Offset: 0x0000A30D
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public ServiceRowChangeEvent(TextMessagingHostingData.ServiceRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000310 RID: 784 RVA: 0x0000C123 File Offset: 0x0000A323
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.ServiceRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000311 RID: 785 RVA: 0x0000C12B File Offset: 0x0000A32B
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x040000F6 RID: 246
			private TextMessagingHostingData.ServiceRow eventRow;

			// Token: 0x040000F7 RID: 247
			private DataRowAction eventAction;
		}

		// Token: 0x0200003C RID: 60
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class VoiceCallForwardingRowChangeEvent : EventArgs
		{
			// Token: 0x06000312 RID: 786 RVA: 0x0000C133 File Offset: 0x0000A333
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public VoiceCallForwardingRowChangeEvent(TextMessagingHostingData.VoiceCallForwardingRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06000313 RID: 787 RVA: 0x0000C149 File Offset: 0x0000A349
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.VoiceCallForwardingRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000314 RID: 788 RVA: 0x0000C151 File Offset: 0x0000A351
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x040000F8 RID: 248
			private TextMessagingHostingData.VoiceCallForwardingRow eventRow;

			// Token: 0x040000F9 RID: 249
			private DataRowAction eventAction;
		}

		// Token: 0x0200003D RID: 61
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class SmtpToSmsGatewayRowChangeEvent : EventArgs
		{
			// Token: 0x06000315 RID: 789 RVA: 0x0000C159 File Offset: 0x0000A359
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public SmtpToSmsGatewayRowChangeEvent(TextMessagingHostingData.SmtpToSmsGatewayRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000316 RID: 790 RVA: 0x0000C16F File Offset: 0x0000A36F
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.SmtpToSmsGatewayRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x06000317 RID: 791 RVA: 0x0000C177 File Offset: 0x0000A377
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x040000FA RID: 250
			private TextMessagingHostingData.SmtpToSmsGatewayRow eventRow;

			// Token: 0x040000FB RID: 251
			private DataRowAction eventAction;
		}

		// Token: 0x0200003E RID: 62
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class RecipientAddressingRowChangeEvent : EventArgs
		{
			// Token: 0x06000318 RID: 792 RVA: 0x0000C17F File Offset: 0x0000A37F
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public RecipientAddressingRowChangeEvent(TextMessagingHostingData.RecipientAddressingRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x06000319 RID: 793 RVA: 0x0000C195 File Offset: 0x0000A395
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.RecipientAddressingRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x0600031A RID: 794 RVA: 0x0000C19D File Offset: 0x0000A39D
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x040000FC RID: 252
			private TextMessagingHostingData.RecipientAddressingRow eventRow;

			// Token: 0x040000FD RID: 253
			private DataRowAction eventAction;
		}

		// Token: 0x0200003F RID: 63
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class MessageRenderingRowChangeEvent : EventArgs
		{
			// Token: 0x0600031B RID: 795 RVA: 0x0000C1A5 File Offset: 0x0000A3A5
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public MessageRenderingRowChangeEvent(TextMessagingHostingData.MessageRenderingRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x0600031C RID: 796 RVA: 0x0000C1BB File Offset: 0x0000A3BB
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public TextMessagingHostingData.MessageRenderingRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x0600031D RID: 797 RVA: 0x0000C1C3 File Offset: 0x0000A3C3
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x040000FE RID: 254
			private TextMessagingHostingData.MessageRenderingRow eventRow;

			// Token: 0x040000FF RID: 255
			private DataRowAction eventAction;
		}

		// Token: 0x02000040 RID: 64
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class CapacityRowChangeEvent : EventArgs
		{
			// Token: 0x0600031E RID: 798 RVA: 0x0000C1CB File Offset: 0x0000A3CB
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public CapacityRowChangeEvent(TextMessagingHostingData.CapacityRow row, DataRowAction action)
			{
				this.eventRow = row;
				this.eventAction = action;
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x0600031F RID: 799 RVA: 0x0000C1E1 File Offset: 0x0000A3E1
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public TextMessagingHostingData.CapacityRow Row
			{
				get
				{
					return this.eventRow;
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x06000320 RID: 800 RVA: 0x0000C1E9 File Offset: 0x0000A3E9
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action
			{
				get
				{
					return this.eventAction;
				}
			}

			// Token: 0x04000100 RID: 256
			private TextMessagingHostingData.CapacityRow eventRow;

			// Token: 0x04000101 RID: 257
			private DataRowAction eventAction;
		}
	}
}
