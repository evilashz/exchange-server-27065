using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x0200001C RID: 28
	internal class DetailsTemplatesSerializationService : IDesignerSerializationService
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x000060C0 File Offset: 0x000042C0
		public DetailsTemplatesSerializationService(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000060D0 File Offset: 0x000042D0
		public ICollection Deserialize(object serializationData)
		{
			SerializationStore serializationStore = serializationData as SerializationStore;
			if (serializationStore != null)
			{
				ComponentSerializationService componentSerializationService = this.serviceProvider.GetService(typeof(ComponentSerializationService)) as ComponentSerializationService;
				return componentSerializationService.Deserialize(serializationStore);
			}
			return new object[0];
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006114 File Offset: 0x00004314
		public object Serialize(ICollection objects)
		{
			ComponentSerializationService componentSerializationService = this.serviceProvider.GetService(typeof(ComponentSerializationService)) as ComponentSerializationService;
			SerializationStore result = null;
			using (SerializationStore serializationStore = componentSerializationService.CreateStore())
			{
				foreach (object obj in objects)
				{
					if (obj is Control)
					{
						componentSerializationService.Serialize(serializationStore, obj);
					}
				}
				result = serializationStore;
			}
			return result;
		}

		// Token: 0x04000057 RID: 87
		private IServiceProvider serviceProvider;
	}
}
