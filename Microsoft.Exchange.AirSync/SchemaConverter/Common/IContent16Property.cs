using System;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000144 RID: 324
	internal interface IContent16Property : IContent14Property, IContentProperty, IMIMEDataProperty, IMIMERelatedProperty, IProperty
	{
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06000FBF RID: 4031
		string BodyString { get; }
	}
}
