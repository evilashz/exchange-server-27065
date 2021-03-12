using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	// Token: 0x02000362 RID: 866
	[ComVisible(true)]
	public interface IResourceWriter : IDisposable
	{
		// Token: 0x06002BCE RID: 11214
		void AddResource(string name, string value);

		// Token: 0x06002BCF RID: 11215
		void AddResource(string name, object value);

		// Token: 0x06002BD0 RID: 11216
		void AddResource(string name, byte[] value);

		// Token: 0x06002BD1 RID: 11217
		void Close();

		// Token: 0x06002BD2 RID: 11218
		void Generate();
	}
}
