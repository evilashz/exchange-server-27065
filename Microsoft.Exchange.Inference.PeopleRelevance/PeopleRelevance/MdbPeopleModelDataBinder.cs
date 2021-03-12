using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000009 RID: 9
	internal class MdbPeopleModelDataBinder : MdbPeopleBaseModelDataBinder<PeopleModelItem>
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002C19 File Offset: 0x00000E19
		public MdbPeopleModelDataBinder(MailboxSession session) : base("Inference.PeopleModel", session)
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002C27 File Offset: 0x00000E27
		protected override Version MinimumSupportedVersion
		{
			get
			{
				return PeopleModelItem.MinimumSupportedVersion;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002C2E File Offset: 0x00000E2E
		internal override Stream GetModelStreamFromUserConfig(UserConfiguration config)
		{
			return config.GetXmlStream();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C36 File Offset: 0x00000E36
		protected override UserConfigurationTypes GetUserConfigurationType()
		{
			return UserConfigurationTypes.XML;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002C39 File Offset: 0x00000E39
		protected override void WriteModelData(DataContractSerializer serializer, Stream stream, PeopleModelItem modelData)
		{
			serializer.WriteObject(stream, modelData);
			stream.SetLength(stream.Position);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C50 File Offset: 0x00000E50
		protected override PeopleModelItem ReadModelData(DataContractSerializer serializer, Stream stream)
		{
			return serializer.ReadObject(stream) as PeopleModelItem;
		}
	}
}
