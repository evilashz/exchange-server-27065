using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000174 RID: 372
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class StatelessCollection
	{
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0001D88E File Offset: 0x0001BA8E
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x0001D896 File Offset: 0x0001BA96
		public string Class
		{
			get
			{
				return this.classField;
			}
			set
			{
				this.classField = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0001D89F File Offset: 0x0001BA9F
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x0001D8A7 File Offset: 0x0001BAA7
		public StatelessCollectionGet Get
		{
			get
			{
				return this.getField;
			}
			set
			{
				this.getField = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0001D8B0 File Offset: 0x0001BAB0
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x0001D8B8 File Offset: 0x0001BAB8
		public uint WindowSize
		{
			get
			{
				return this.windowSizeField;
			}
			set
			{
				this.windowSizeField = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0001D8C1 File Offset: 0x0001BAC1
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x0001D8C9 File Offset: 0x0001BAC9
		[XmlIgnore]
		public bool WindowSizeSpecified
		{
			get
			{
				return this.windowSizeFieldSpecified;
			}
			set
			{
				this.windowSizeFieldSpecified = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0001D8D2 File Offset: 0x0001BAD2
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x0001D8DA File Offset: 0x0001BADA
		public string CollectionId
		{
			get
			{
				return this.collectionIdField;
			}
			set
			{
				this.collectionIdField = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0001D8E3 File Offset: 0x0001BAE3
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x0001D8EB File Offset: 0x0001BAEB
		public StatelessCollectionCommands Commands
		{
			get
			{
				return this.commandsField;
			}
			set
			{
				this.commandsField = value;
			}
		}

		// Token: 0x04000615 RID: 1557
		private string classField;

		// Token: 0x04000616 RID: 1558
		private StatelessCollectionGet getField;

		// Token: 0x04000617 RID: 1559
		private uint windowSizeField;

		// Token: 0x04000618 RID: 1560
		private bool windowSizeFieldSpecified;

		// Token: 0x04000619 RID: 1561
		private string collectionIdField;

		// Token: 0x0400061A RID: 1562
		private StatelessCollectionCommands commandsField;
	}
}
