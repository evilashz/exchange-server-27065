using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.Cafe
{
	// Token: 0x020000B5 RID: 181
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class VDirInfo
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0001DFF0 File Offset: 0x0001C1F0
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x0001DFF8 File Offset: 0x0001C1F8
		internal Uri ExternalUri { get; private set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001E001 File Offset: 0x0001C201
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0001E009 File Offset: 0x0001C209
		internal string Path { get; private set; }

		// Token: 0x060007BB RID: 1979 RVA: 0x0001E012 File Offset: 0x0001C212
		internal VDirInfo(Uri externalUri)
		{
			ArgumentValidator.ThrowIfNull("externalUri", externalUri);
			this.ExternalUri = externalUri;
			this.Path = this.ExternalUri.GetComponents(UriComponents.Path, UriFormat.UriEscaped);
		}
	}
}
