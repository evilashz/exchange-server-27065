using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009DE RID: 2526
	[Guid("7C925755-3E48-42B4-8677-76372267033F")]
	[ComImport]
	internal interface ICustomPropertyProvider
	{
		// Token: 0x06006464 RID: 25700
		ICustomProperty GetCustomProperty(string name);

		// Token: 0x06006465 RID: 25701
		ICustomProperty GetIndexedProperty(string name, Type indexParameterType);

		// Token: 0x06006466 RID: 25702
		string GetStringRepresentation();

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x06006467 RID: 25703
		Type Type { get; }
	}
}
