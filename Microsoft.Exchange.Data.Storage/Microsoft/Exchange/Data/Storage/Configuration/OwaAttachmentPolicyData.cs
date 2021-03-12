using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Configuration
{
	// Token: 0x02000464 RID: 1124
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class OwaAttachmentPolicyData : SerializableDataBase, IEquatable<OwaAttachmentPolicyData>
	{
		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06003227 RID: 12839 RVA: 0x000CD364 File Offset: 0x000CB564
		// (set) Token: 0x06003228 RID: 12840 RVA: 0x000CD36C File Offset: 0x000CB56C
		[DataMember]
		public string[] BlockFileTypes { get; set; }

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x000CD375 File Offset: 0x000CB575
		// (set) Token: 0x0600322A RID: 12842 RVA: 0x000CD37D File Offset: 0x000CB57D
		[DataMember]
		public string[] BlockMimeTypes { get; set; }

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x0600322B RID: 12843 RVA: 0x000CD386 File Offset: 0x000CB586
		// (set) Token: 0x0600322C RID: 12844 RVA: 0x000CD38E File Offset: 0x000CB58E
		[DataMember]
		public string[] ForceSaveFileTypes { get; set; }

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x0600322D RID: 12845 RVA: 0x000CD397 File Offset: 0x000CB597
		// (set) Token: 0x0600322E RID: 12846 RVA: 0x000CD39F File Offset: 0x000CB59F
		[DataMember]
		public string[] ForceSaveMimeTypes { get; set; }

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x000CD3A8 File Offset: 0x000CB5A8
		// (set) Token: 0x06003230 RID: 12848 RVA: 0x000CD3B0 File Offset: 0x000CB5B0
		[DataMember]
		public string[] AllowFileTypes { get; set; }

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x000CD3B9 File Offset: 0x000CB5B9
		// (set) Token: 0x06003232 RID: 12850 RVA: 0x000CD3C1 File Offset: 0x000CB5C1
		[DataMember]
		public string[] AllowMimeTypes { get; set; }

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000CD3CA File Offset: 0x000CB5CA
		// (set) Token: 0x06003234 RID: 12852 RVA: 0x000CD3D2 File Offset: 0x000CB5D2
		[DataMember]
		public string TreatUnknownTypeAs { get; set; }

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x000CD3DB File Offset: 0x000CB5DB
		// (set) Token: 0x06003236 RID: 12854 RVA: 0x000CD3E3 File Offset: 0x000CB5E3
		[DataMember]
		public bool DirectFileAccessOnPublicComputersEnabled { get; set; }

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x000CD3EC File Offset: 0x000CB5EC
		// (set) Token: 0x06003238 RID: 12856 RVA: 0x000CD3F4 File Offset: 0x000CB5F4
		[DataMember]
		public bool DirectFileAccessOnPrivateComputersEnabled { get; set; }

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06003239 RID: 12857 RVA: 0x000CD3FD File Offset: 0x000CB5FD
		// (set) Token: 0x0600323A RID: 12858 RVA: 0x000CD405 File Offset: 0x000CB605
		[DataMember]
		public bool ForceWacViewingFirstOnPublicComputers { get; set; }

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x000CD40E File Offset: 0x000CB60E
		// (set) Token: 0x0600323C RID: 12860 RVA: 0x000CD416 File Offset: 0x000CB616
		[DataMember]
		public bool ForceWacViewingFirstOnPrivateComputers { get; set; }

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x000CD41F File Offset: 0x000CB61F
		// (set) Token: 0x0600323E RID: 12862 RVA: 0x000CD427 File Offset: 0x000CB627
		[DataMember]
		public bool WacViewingOnPublicComputersEnabled { get; set; }

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x000CD430 File Offset: 0x000CB630
		// (set) Token: 0x06003240 RID: 12864 RVA: 0x000CD438 File Offset: 0x000CB638
		[DataMember]
		public bool WacViewingOnPrivateComputersEnabled { get; set; }

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06003241 RID: 12865 RVA: 0x000CD441 File Offset: 0x000CB641
		// (set) Token: 0x06003242 RID: 12866 RVA: 0x000CD449 File Offset: 0x000CB649
		[DataMember]
		public bool ForceWebReadyDocumentViewingFirstOnPublicComputers { get; set; }

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06003243 RID: 12867 RVA: 0x000CD452 File Offset: 0x000CB652
		// (set) Token: 0x06003244 RID: 12868 RVA: 0x000CD45A File Offset: 0x000CB65A
		[DataMember]
		public bool ForceWebReadyDocumentViewingFirstOnPrivateComputers { get; set; }

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06003245 RID: 12869 RVA: 0x000CD463 File Offset: 0x000CB663
		// (set) Token: 0x06003246 RID: 12870 RVA: 0x000CD46B File Offset: 0x000CB66B
		[DataMember]
		public bool WebReadyDocumentViewingOnPublicComputersEnabled { get; set; }

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x000CD474 File Offset: 0x000CB674
		// (set) Token: 0x06003248 RID: 12872 RVA: 0x000CD47C File Offset: 0x000CB67C
		[DataMember]
		public bool WebReadyDocumentViewingOnPrivateComputersEnabled { get; set; }

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06003249 RID: 12873 RVA: 0x000CD485 File Offset: 0x000CB685
		// (set) Token: 0x0600324A RID: 12874 RVA: 0x000CD48D File Offset: 0x000CB68D
		[DataMember]
		public string[] WebReadyFileTypes { get; set; }

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x000CD496 File Offset: 0x000CB696
		// (set) Token: 0x0600324C RID: 12876 RVA: 0x000CD49E File Offset: 0x000CB69E
		[DataMember]
		public string[] WebReadyMimeTypes { get; set; }

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x0600324D RID: 12877 RVA: 0x000CD4A7 File Offset: 0x000CB6A7
		// (set) Token: 0x0600324E RID: 12878 RVA: 0x000CD4AF File Offset: 0x000CB6AF
		[DataMember]
		public string[] WebReadyDocumentViewingSupportedFileTypes { get; set; }

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x0600324F RID: 12879 RVA: 0x000CD4B8 File Offset: 0x000CB6B8
		// (set) Token: 0x06003250 RID: 12880 RVA: 0x000CD4C0 File Offset: 0x000CB6C0
		[DataMember]
		public string[] WebReadyDocumentViewingSupportedMimeTypes { get; set; }

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06003251 RID: 12881 RVA: 0x000CD4C9 File Offset: 0x000CB6C9
		// (set) Token: 0x06003252 RID: 12882 RVA: 0x000CD4D1 File Offset: 0x000CB6D1
		[DataMember]
		public bool WebReadyDocumentViewingForAllSupportedTypes { get; set; }

		// Token: 0x06003253 RID: 12883 RVA: 0x000CD4DC File Offset: 0x000CB6DC
		public bool Equals(OwaAttachmentPolicyData other)
		{
			return !object.ReferenceEquals(other, null) && (object.ReferenceEquals(other, this) || (SerializableDataBase.ArrayContentsEquals<string>(this.BlockFileTypes, other.BlockFileTypes) && SerializableDataBase.ArrayContentsEquals<string>(this.BlockMimeTypes, other.BlockMimeTypes) && SerializableDataBase.ArrayContentsEquals<string>(this.ForceSaveFileTypes, other.ForceSaveFileTypes) && SerializableDataBase.ArrayContentsEquals<string>(this.ForceSaveMimeTypes, other.ForceSaveMimeTypes) && SerializableDataBase.ArrayContentsEquals<string>(this.AllowFileTypes, other.AllowFileTypes) && SerializableDataBase.ArrayContentsEquals<string>(this.AllowMimeTypes, other.AllowMimeTypes) && this.TreatUnknownTypeAs == other.TreatUnknownTypeAs && this.DirectFileAccessOnPublicComputersEnabled == other.DirectFileAccessOnPublicComputersEnabled && this.DirectFileAccessOnPrivateComputersEnabled == other.DirectFileAccessOnPrivateComputersEnabled && this.ForceWacViewingFirstOnPublicComputers == other.ForceWacViewingFirstOnPublicComputers && this.ForceWacViewingFirstOnPrivateComputers == other.ForceWacViewingFirstOnPrivateComputers && this.WacViewingOnPublicComputersEnabled == other.WacViewingOnPublicComputersEnabled && this.WacViewingOnPrivateComputersEnabled == other.WacViewingOnPrivateComputersEnabled && this.ForceWebReadyDocumentViewingFirstOnPublicComputers == other.ForceWebReadyDocumentViewingFirstOnPublicComputers && this.ForceWebReadyDocumentViewingFirstOnPrivateComputers == other.ForceWebReadyDocumentViewingFirstOnPrivateComputers && this.WebReadyDocumentViewingOnPublicComputersEnabled == other.WebReadyDocumentViewingOnPublicComputersEnabled && this.WebReadyDocumentViewingOnPrivateComputersEnabled == other.WebReadyDocumentViewingOnPrivateComputersEnabled && SerializableDataBase.ArrayContentsEquals<string>(this.WebReadyFileTypes, other.WebReadyFileTypes) && SerializableDataBase.ArrayContentsEquals<string>(this.WebReadyMimeTypes, other.WebReadyMimeTypes) && SerializableDataBase.ArrayContentsEquals<string>(this.WebReadyDocumentViewingSupportedFileTypes, other.WebReadyDocumentViewingSupportedFileTypes) && SerializableDataBase.ArrayContentsEquals<string>(this.WebReadyDocumentViewingSupportedMimeTypes, other.WebReadyDocumentViewingSupportedMimeTypes) && this.WebReadyDocumentViewingForAllSupportedTypes == other.WebReadyDocumentViewingForAllSupportedTypes));
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000CD696 File Offset: 0x000CB896
		protected override bool InternalEquals(object other)
		{
			return this.Equals(other as OwaAttachmentPolicyData);
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000CD6A4 File Offset: 0x000CB8A4
		protected override int InternalGetHashCode()
		{
			int num = 17;
			num = (num * 397 ^ SerializableDataBase.ArrayContentsHash<string>(this.BlockFileTypes));
			num = (num * 397 ^ SerializableDataBase.ArrayContentsHash<string>(this.BlockMimeTypes));
			num = (num * 397 ^ SerializableDataBase.ArrayContentsHash<string>(this.ForceSaveFileTypes));
			num = (num * 397 ^ SerializableDataBase.ArrayContentsHash<string>(this.ForceSaveMimeTypes));
			num = (num * 397 ^ SerializableDataBase.ArrayContentsHash<string>(this.AllowFileTypes));
			num = (num * 397 ^ SerializableDataBase.ArrayContentsHash<string>(this.AllowMimeTypes));
			num = (num * 397 ^ ((this.TreatUnknownTypeAs == null) ? 0 : this.TreatUnknownTypeAs.GetHashCode()));
			num = (num * 397 ^ this.DirectFileAccessOnPublicComputersEnabled.GetHashCode());
			num = (num * 397 ^ this.DirectFileAccessOnPrivateComputersEnabled.GetHashCode());
			num = (num * 397 ^ this.ForceWacViewingFirstOnPublicComputers.GetHashCode());
			num = (num * 397 ^ this.ForceWacViewingFirstOnPrivateComputers.GetHashCode());
			num = (num * 397 ^ this.WacViewingOnPublicComputersEnabled.GetHashCode());
			num = (num * 397 ^ this.WacViewingOnPrivateComputersEnabled.GetHashCode());
			num = (num * 397 ^ this.ForceWebReadyDocumentViewingFirstOnPublicComputers.GetHashCode());
			num = (num * 397 ^ this.ForceWebReadyDocumentViewingFirstOnPrivateComputers.GetHashCode());
			num = (num * 397 ^ this.WebReadyDocumentViewingOnPublicComputersEnabled.GetHashCode());
			num = (num * 397 ^ this.WebReadyDocumentViewingOnPrivateComputersEnabled.GetHashCode());
			num = (num * 397 ^ SerializableDataBase.ArrayContentsHash<string>(this.WebReadyFileTypes));
			num = (num * 397 ^ SerializableDataBase.ArrayContentsHash<string>(this.WebReadyMimeTypes));
			num = (num * 397 ^ SerializableDataBase.ArrayContentsHash<string>(this.WebReadyDocumentViewingSupportedFileTypes));
			num = (num * 397 ^ SerializableDataBase.ArrayContentsHash<string>(this.WebReadyDocumentViewingSupportedMimeTypes));
			return num * 397 ^ this.WebReadyDocumentViewingForAllSupportedTypes.GetHashCode();
		}
	}
}
