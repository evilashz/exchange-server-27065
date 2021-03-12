using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200082D RID: 2093
	[Serializable]
	public sealed class ExportableClassificationRuleCollectionPresentationObject : ClassificationRuleCollectionPresentationObject
	{
		// Token: 0x0600488B RID: 18571 RVA: 0x001298D4 File Offset: 0x00127AD4
		private ExportableClassificationRuleCollectionPresentationObject(TransportRule transportRule) : base(transportRule)
		{
		}

		// Token: 0x0600488C RID: 18572 RVA: 0x001298E0 File Offset: 0x00127AE0
		internal new static ExportableClassificationRuleCollectionPresentationObject Create(TransportRule transportRule)
		{
			if (transportRule == null)
			{
				throw new ArgumentNullException("transportRule");
			}
			ExportableClassificationRuleCollectionPresentationObject exportableClassificationRuleCollectionPresentationObject = new ExportableClassificationRuleCollectionPresentationObject(transportRule);
			exportableClassificationRuleCollectionPresentationObject.Initialize();
			return exportableClassificationRuleCollectionPresentationObject;
		}

		// Token: 0x0600488D RID: 18573 RVA: 0x0012990C File Offset: 0x00127B0C
		internal byte[] Export()
		{
			if (!this.IsExportable)
			{
				throw new InvalidOperationException(Strings.ClassificationRuleCollectionExportEncyrptedProhibited);
			}
			TransportRule storedRuleCollection = base.StoredRuleCollection;
			ExAssert.RetailAssert(storedRuleCollection != null, "The classification rule collection presentation object should store reference to its backing transport rule instance.");
			byte[] result;
			Exception ex;
			bool condition = ClassificationDefinitionUtils.TryUncompressXmlBytes(storedRuleCollection.ReplicationSignature, out result, out ex);
			ExAssert.RetailAssert(condition, "Decompression of classification rule collection must succeed since the presentation was initialized successfully before. Details: {0}", new object[]
			{
				(ex != null) ? ex.ToString() : string.Empty
			});
			return result;
		}

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x0600488E RID: 18574 RVA: 0x00129983 File Offset: 0x00127B83
		public bool IsExportable
		{
			get
			{
				return !base.IsEncrypted;
			}
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x0600488F RID: 18575 RVA: 0x00129990 File Offset: 0x00127B90
		public byte[] SerializedClassificationRuleCollection
		{
			get
			{
				byte[] result;
				try
				{
					result = this.Export();
				}
				catch (InvalidOperationException)
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06004890 RID: 18576 RVA: 0x001299BC File Offset: 0x00127BBC
		private new Guid Guid
		{
			get
			{
				return base.Guid;
			}
		}

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x06004891 RID: 18577 RVA: 0x001299C4 File Offset: 0x00127BC4
		private new Guid ImmutableId
		{
			get
			{
				return base.ImmutableId;
			}
		}
	}
}
