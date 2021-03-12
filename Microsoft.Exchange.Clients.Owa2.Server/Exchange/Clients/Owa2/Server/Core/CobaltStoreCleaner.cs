using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000040 RID: 64
	public class CobaltStoreCleaner
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00006B5A File Offset: 0x00004D5A
		private CobaltStoreCleaner(TimeSpan cleaningInterval, TimeSpan expirationTime)
		{
			this.cleaningInterval = cleaningInterval;
			this.expirationTime = expirationTime;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006B7C File Offset: 0x00004D7C
		public static CobaltStoreCleaner GetInstance()
		{
			if (CobaltStoreCleaner.singleton == null)
			{
				lock (CobaltStoreCleaner.syncObject)
				{
					if (CobaltStoreCleaner.singleton == null)
					{
						CobaltStoreCleaner.singleton = new CobaltStoreCleaner(WacConfiguration.Instance.CobaltStoreCleanupInterval, WacConfiguration.Instance.CobaltStoreExpirationInterval);
					}
				}
			}
			return CobaltStoreCleaner.singleton;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006BE8 File Offset: 0x00004DE8
		internal static CobaltStoreCleaner GetTestInstance(TimeSpan cleaningInterval, TimeSpan expirationTime)
		{
			return new CobaltStoreCleaner(cleaningInterval, expirationTime);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006C70 File Offset: 0x00004E70
		public void Clean(IList<string> rootDirectories)
		{
			if (this.IsCleaningOverdue())
			{
				lock (CobaltStoreCleaner.syncObject)
				{
					if (this.IsCleaningOverdue())
					{
						SimulatedWebRequestContext.ExecuteWithoutUserContext("WAC.CleanCobaltStore", delegate(RequestDetailsLogger logger)
						{
							WacUtilities.SetEventId(logger, "WAC.CleanCobaltStore");
							foreach (string text in rootDirectories)
							{
								CobaltStoreCleaner.ValidatePath(text);
								try
								{
									this.CleanRootDirectory(text);
								}
								catch (DirectoryNotFoundException)
								{
								}
							}
						});
					}
				}
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006CEC File Offset: 0x00004EEC
		private static void ValidatePath(string path)
		{
			if (!path.Contains("OwaCobalt"))
			{
				throw new InvalidOperationException("This path is not in the expected directory: " + path);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006D0C File Offset: 0x00004F0C
		private bool IsCleaningOverdue()
		{
			ExDateTime t = this.lastCleaning + this.cleaningInterval;
			return ExDateTime.UtcNow > t;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006D38 File Offset: 0x00004F38
		private void CleanRootDirectory(string rootDirectory)
		{
			string[] directories = Directory.GetDirectories(rootDirectory);
			foreach (string cobaltStoreDirectory in directories)
			{
				this.CleanCobaltStoreDirectory(cobaltStoreDirectory);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006D68 File Offset: 0x00004F68
		private void CleanCobaltStoreDirectory(string cobaltStoreDirectory)
		{
			ExDateTime oldestFileTime = this.GetOldestFileTime(cobaltStoreDirectory);
			ExDateTime t = ExDateTime.UtcNow.Subtract(this.expirationTime);
			if (oldestFileTime < t)
			{
				CobaltStoreCleaner.ValidatePath(cobaltStoreDirectory);
				try
				{
					Directory.Delete(cobaltStoreDirectory, true);
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006DBC File Offset: 0x00004FBC
		private ExDateTime GetOldestFileTime(string cobaltStoreDirectory)
		{
			ExDateTime exDateTime = ExDateTime.MinValue;
			string[] files = Directory.GetFiles(cobaltStoreDirectory, "*", SearchOption.AllDirectories);
			foreach (string path in files)
			{
				ExDateTime exDateTime2 = new ExDateTime(ExTimeZone.UtcTimeZone, File.GetLastAccessTimeUtc(path));
				if (exDateTime2 > exDateTime)
				{
					exDateTime = exDateTime2;
				}
			}
			return exDateTime;
		}

		// Token: 0x040000A3 RID: 163
		private static readonly object syncObject = new object();

		// Token: 0x040000A4 RID: 164
		private static CobaltStoreCleaner singleton;

		// Token: 0x040000A5 RID: 165
		private readonly TimeSpan cleaningInterval;

		// Token: 0x040000A6 RID: 166
		private readonly TimeSpan expirationTime;

		// Token: 0x040000A7 RID: 167
		private ExDateTime lastCleaning = ExDateTime.MinValue;
	}
}
