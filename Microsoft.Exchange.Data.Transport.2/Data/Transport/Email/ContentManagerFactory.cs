using System;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x0200010B RID: 267
	internal class ContentManagerFactory
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x0001E284 File Offset: 0x0001C484
		private ContentManagerFactory()
		{
			try
			{
				Assembly.Load("Microsoft.Exchange.UnifiedContent");
				this.installed = true;
			}
			catch (FileNotFoundException)
			{
				this.installed = false;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
		internal static ContentManagerFactory Instance
		{
			get
			{
				return ContentManagerFactory.instance;
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001E2CF File Offset: 0x0001C4CF
		internal static IDisposable ExtractContentManager(EmailMessage msg)
		{
			if (msg.ContentManager == null)
			{
				msg.ContentManager = ContentManagerFactory.Instance.Create();
				if (msg.ContentManager == null)
				{
					throw new InvalidOperationException("ContentManager is not available");
				}
			}
			return msg.ContentManager;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001E304 File Offset: 0x0001C504
		internal IDisposable Create()
		{
			if (!this.installed)
			{
				return null;
			}
			ObjectHandle objectHandle = Activator.CreateInstance("Microsoft.Exchange.UnifiedContent", "Microsoft.Exchange.UnifiedContent.ContentManager", false, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[]
			{
				this.GetTempFilePath()
			}, null, null);
			return (IDisposable)objectHandle.Unwrap();
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001E34D File Offset: 0x0001C54D
		private string GetTempFilePath()
		{
			return TemporaryDataStorage.GetTempPath();
		}

		// Token: 0x0400046D RID: 1133
		private const string AssemblyNameLoad = "Microsoft.Exchange.UnifiedContent";

		// Token: 0x0400046E RID: 1134
		private const string TheType = "Microsoft.Exchange.UnifiedContent.ContentManager";

		// Token: 0x0400046F RID: 1135
		private static readonly ContentManagerFactory instance = new ContentManagerFactory();

		// Token: 0x04000470 RID: 1136
		private readonly bool installed;
	}
}
