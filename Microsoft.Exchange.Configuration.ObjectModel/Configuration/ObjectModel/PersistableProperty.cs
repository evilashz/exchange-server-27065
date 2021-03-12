using System;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	internal class PersistableProperty : SchemaMappingEntry
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public PersistableProperty()
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005AAC File Offset: 0x00003CAC
		public PersistableProperty(string sourceClassName, string sourcePropertyName)
		{
			this.sourceClassName = sourceClassName;
			this.sourcePropertyName = sourcePropertyName;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005AC2 File Offset: 0x00003CC2
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00005ACA File Offset: 0x00003CCA
		public string SourceClassName
		{
			get
			{
				return this.sourceClassName;
			}
			set
			{
				this.sourceClassName = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005AD3 File Offset: 0x00003CD3
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00005ADB File Offset: 0x00003CDB
		public string SourcePropertyName
		{
			get
			{
				return this.sourcePropertyName;
			}
			set
			{
				this.sourcePropertyName = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00005AE4 File Offset: 0x00003CE4
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00005AEC File Offset: 0x00003CEC
		public string PropertyTypeName
		{
			get
			{
				return this.propertyTypeName;
			}
			set
			{
				this.propertyTypeName = value;
				this.propertyType = Type.GetType(this.propertyTypeName);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00005B06 File Offset: 0x00003D06
		public Type PropertyType
		{
			get
			{
				return this.propertyType;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00005B0E File Offset: 0x00003D0E
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00005B16 File Offset: 0x00003D16
		public string StoragePropertyTypeName
		{
			get
			{
				return this.storagePropertyTypeName;
			}
			set
			{
				this.storagePropertyTypeName = value;
				this.storagePropertyType = Type.GetType(this.storagePropertyTypeName);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005B30 File Offset: 0x00003D30
		public Type StoragePropertyType
		{
			get
			{
				return this.storagePropertyType;
			}
		}

		// Token: 0x04000062 RID: 98
		private string sourceClassName;

		// Token: 0x04000063 RID: 99
		private string sourcePropertyName;

		// Token: 0x04000064 RID: 100
		private string propertyTypeName;

		// Token: 0x04000065 RID: 101
		private Type propertyType;

		// Token: 0x04000066 RID: 102
		private string storagePropertyTypeName;

		// Token: 0x04000067 RID: 103
		private Type storagePropertyType;
	}
}
