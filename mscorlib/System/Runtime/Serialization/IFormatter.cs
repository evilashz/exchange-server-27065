using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000705 RID: 1797
	[ComVisible(true)]
	public interface IFormatter
	{
		// Token: 0x06005087 RID: 20615
		object Deserialize(Stream serializationStream);

		// Token: 0x06005088 RID: 20616
		void Serialize(Stream serializationStream, object graph);

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06005089 RID: 20617
		// (set) Token: 0x0600508A RID: 20618
		ISurrogateSelector SurrogateSelector { get; set; }

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x0600508B RID: 20619
		// (set) Token: 0x0600508C RID: 20620
		SerializationBinder Binder { get; set; }

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x0600508D RID: 20621
		// (set) Token: 0x0600508E RID: 20622
		StreamingContext Context { get; set; }
	}
}
