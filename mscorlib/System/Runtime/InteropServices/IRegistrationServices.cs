using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200093D RID: 2365
	[Guid("CCBD682C-73A5-4568-B8B0-C7007E11ABA2")]
	[ComVisible(true)]
	public interface IRegistrationServices
	{
		// Token: 0x06006110 RID: 24848
		[SecurityCritical]
		bool RegisterAssembly(Assembly assembly, AssemblyRegistrationFlags flags);

		// Token: 0x06006111 RID: 24849
		[SecurityCritical]
		bool UnregisterAssembly(Assembly assembly);

		// Token: 0x06006112 RID: 24850
		[SecurityCritical]
		Type[] GetRegistrableTypesInAssembly(Assembly assembly);

		// Token: 0x06006113 RID: 24851
		[SecurityCritical]
		string GetProgIdForType(Type type);

		// Token: 0x06006114 RID: 24852
		[SecurityCritical]
		void RegisterTypeForComClients(Type type, ref Guid g);

		// Token: 0x06006115 RID: 24853
		Guid GetManagedCategoryGuid();

		// Token: 0x06006116 RID: 24854
		[SecurityCritical]
		bool TypeRequiresRegistration(Type type);

		// Token: 0x06006117 RID: 24855
		bool TypeRepresentsComType(Type type);
	}
}
