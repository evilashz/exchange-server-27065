using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000026 RID: 38
	internal class MdbMaskedPeopleModelDataBinder : MdbPeopleBaseModelDataBinder<MaskedPeopleModelItem>
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00006FF8 File Offset: 0x000051F8
		public MdbMaskedPeopleModelDataBinder(MailboxSession session) : base("Inference.MaskedPeopleModel", session)
		{
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00007006 File Offset: 0x00005206
		protected override Version MinimumSupportedVersion
		{
			get
			{
				return MaskedPeopleModelItem.MinimumSupportedVersion;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000700D File Offset: 0x0000520D
		internal override Stream GetModelStreamFromUserConfig(UserConfiguration config)
		{
			return config.GetXmlStream();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007015 File Offset: 0x00005215
		protected override UserConfigurationTypes GetUserConfigurationType()
		{
			return UserConfigurationTypes.XML;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007018 File Offset: 0x00005218
		protected override void WriteModelData(DataContractSerializer serializer, Stream stream, MaskedPeopleModelItem modelData)
		{
			serializer.WriteObject(stream, modelData);
			stream.SetLength(stream.Position);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000702E File Offset: 0x0000522E
		protected override MaskedPeopleModelItem ReadModelData(DataContractSerializer serializer, Stream stream)
		{
			return serializer.ReadObject(stream) as MaskedPeopleModelItem;
		}
	}
}
