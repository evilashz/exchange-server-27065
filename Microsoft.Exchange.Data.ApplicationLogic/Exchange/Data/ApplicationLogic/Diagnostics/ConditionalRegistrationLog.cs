using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000D3 RID: 211
	internal class ConditionalRegistrationLog
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00023D07 File Offset: 0x00021F07
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x00023D0E File Offset: 0x00021F0E
		internal static Action<XElement, Exception> OnFailedHydration { get; set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00023D16 File Offset: 0x00021F16
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x00023D1D File Offset: 0x00021F1D
		public static string ProtocolName
		{
			get
			{
				return ConditionalRegistrationLog.protocolName;
			}
			set
			{
				ConditionalRegistrationLog.protocolName = value;
				ConditionalRegistrationLog.LoggingDirectory = Path.Combine(ConditionalRegistrationLog.LoggingRootPath, ConditionalRegistrationLog.protocolName);
				ConditionalRegistrationLog.CreateLogFilePath();
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00023D40 File Offset: 0x00021F40
		static ConditionalRegistrationLog()
		{
			ConditionalRegistrationLog.LoggingRootPath = Path.Combine(ExchangeSetupContext.LoggingPath, "ConditionalHandlers");
			ConditionalRegistrationLog.CreateLogFilePath();
			ConditionalRegistrationLog.writerSettings = new XmlWriterSettings();
			ConditionalRegistrationLog.writerSettings.CheckCharacters = false;
			ConditionalRegistrationLog.writerSettings.CloseOutput = true;
			ConditionalRegistrationLog.writerSettings.ConformanceLevel = ConformanceLevel.Fragment;
			ConditionalRegistrationLog.writerSettings.Indent = true;
			ConditionalRegistrationLog.readerSettings = new XmlReaderSettings();
			ConditionalRegistrationLog.readerSettings.CheckCharacters = false;
			ConditionalRegistrationLog.readerSettings.ConformanceLevel = ConformanceLevel.Fragment;
			ConditionalRegistrationLog.readerSettings.DtdProcessing = DtdProcessing.Prohibit;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00023DF8 File Offset: 0x00021FF8
		public static string Save(ConditionalRegistration registration)
		{
			string text = Path.Combine(ConditionalRegistrationLog.GetConditionalRegistrationsDirectory(), string.Format("{0}.{1}", registration.Cookie, "xml"));
			ExTraceGlobals.DiagnosticHandlersTracer.TraceDebug<string, string>(0L, "[ConditionalRegistrationLog.Save] Logging registration for '{0}' of user '{1}'", registration.Cookie, registration.User);
			using (XmlWriter xmlWriter = XmlWriter.Create(text, ConditionalRegistrationLog.writerSettings))
			{
				XElement xelement = new XElement("ConditionalRegistration");
				XElement content = new XElement("Description")
				{
					Value = registration.Description
				};
				xelement.Add(content);
				XElement content2 = new XElement("CreatedTime")
				{
					Value = registration.Created.ToString()
				};
				xelement.Add(content2);
				XElement content3 = new XElement("User")
				{
					Value = registration.User
				};
				xelement.Add(content3);
				XElement content4 = new XElement("Cookie")
				{
					Value = registration.Cookie
				};
				xelement.Add(content4);
				XElement content5 = new XElement("PropertiesToFetch")
				{
					Value = registration.OriginalPropertiesToFetch
				};
				xelement.Add(content5);
				XElement content6 = new XElement("Filter")
				{
					Value = registration.OriginalFilter
				};
				xelement.Add(content6);
				XElement content7 = new XElement("TimeToLive")
				{
					Value = registration.TimeToLive.ToString()
				};
				xelement.Add(content7);
				XElement content8 = new XElement("MaxHits")
				{
					Value = registration.MaxHits.ToString()
				};
				xelement.Add(content8);
				xelement.WriteTo(xmlWriter);
				xmlWriter.Flush();
			}
			return text;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0002400C File Offset: 0x0002220C
		public static void DeleteRegistration(string cookie)
		{
			string path = Path.Combine(ConditionalRegistrationLog.GetConditionalRegistrationsDirectory(), string.Format("{0}.{1}", cookie, "xml"));
			ExTraceGlobals.DiagnosticHandlersTracer.TraceDebug<string>(0L, "[ConditionalRegistrationLog.DeleteRegistration] Deleting registration for '{0}'", cookie);
			if (File.Exists(path))
			{
				try
				{
					File.Delete(path);
				}
				catch (Exception ex)
				{
					ExTraceGlobals.DiagnosticHandlersTracer.TraceDebug<string, string>(0L, "[ConditionalRegistrationLog.DeleteRegistration] failure deleting registration for '{0}'. Exception: {1}", cookie, ex.ToString());
				}
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00024084 File Offset: 0x00022284
		public static string Save(ConditionalResults hit)
		{
			string fullPathForCookie = ConditionalRegistrationLog.GetFullPathForCookie(hit.Registration.User.Replace("/", "-"), hit.Registration.Cookie);
			ConditionalRegistrationLog.LimitFileCount(fullPathForCookie);
			ExTraceGlobals.DiagnosticHandlersTracer.TraceDebug<string, string>(0L, "[ConditionalRegistrationLog.Log] Logging registration results for '{0}' to path '{1}'", hit.Registration.Cookie, fullPathForCookie);
			try
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(fullPathForCookie, ConditionalRegistrationLog.writerSettings))
				{
					hit.GetXmlResults().WriteTo(xmlWriter);
					xmlWriter.Flush();
				}
			}
			catch (Exception arg)
			{
				ExTraceGlobals.DiagnosticHandlersTracer.TraceDebug<string, Exception>(0L, "[ConditionalRegistrationLog.Log] Caught exception trying to save hit for cookie: {0}.  Exception: {1}", hit.Registration.Cookie, arg);
			}
			return fullPathForCookie;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00024148 File Offset: 0x00022348
		public static void LogFailedHydration(XElement childNode, Exception exception)
		{
			if (ConditionalRegistrationLog.OnFailedHydration != null)
			{
				ConditionalRegistrationLog.OnFailedHydration(childNode, exception);
				return;
			}
			ConditionalRegistrationLog.EventLog.LogEvent(ApplicationLogicEventLogConstants.Tuple_PersistentHandlerRegistrationFailed, null, new object[]
			{
				childNode.ToString(SaveOptions.None),
				ConditionalRegistrationLog.ProtocolName,
				exception.ToString()
			});
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x000241C4 File Offset: 0x000223C4
		private static void LimitFileCount(string path)
		{
			string directoryName = Path.GetDirectoryName(path);
			DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
			string extension = Path.GetExtension(path);
			FileInfo[] files = directoryInfo.GetFiles("*" + extension);
			int num = files.Length - ConditionalRegistrationLog.MaxFiles.Value + 1;
			if (num > 0)
			{
				ExTraceGlobals.DiagnosticHandlersTracer.TraceDebug<int, string>(0L, "[ConditionalRegistrationLog.LimitFileCount] Deleting {0} old files from directory: {1}", num, directoryName);
				Array.Sort<FileInfo>(files, (FileInfo file1, FileInfo file2) => file1.CreationTimeUtc.CompareTo(file2.CreationTimeUtc));
				for (int i = 0; i < num; i++)
				{
					try
					{
						files[i].Delete();
					}
					catch (Exception ex)
					{
						ExTraceGlobals.DiagnosticHandlersTracer.TraceError<string, string>(0L, "[ConditionalRegistrationLog.LimitFileCount] Failed to delete file '{0}' with exception '{1}'.", files[i].FullName, ex.ToString());
					}
				}
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00024298 File Offset: 0x00022498
		public static List<ConditionalRegistrationLog.ConditionalRegistrationHitMetadata> GetHitsMetadata(string userIdentity = "")
		{
			List<ConditionalRegistrationLog.ConditionalRegistrationHitMetadata> list = null;
			string text = Path.Combine(ConditionalRegistrationLog.LoggingDirectory, userIdentity);
			if (!string.IsNullOrEmpty(userIdentity))
			{
				list = ConditionalRegistrationLog.GetHitsMetadataForUser(text, userIdentity);
			}
			else
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(text);
				DirectoryInfo[] directories = directoryInfo.GetDirectories();
				foreach (DirectoryInfo directoryInfo2 in directories)
				{
					List<ConditionalRegistrationLog.ConditionalRegistrationHitMetadata> hitsMetadataForUser = ConditionalRegistrationLog.GetHitsMetadataForUser(directoryInfo2.FullName, directoryInfo2.Name);
					if (hitsMetadataForUser != null)
					{
						if (list == null)
						{
							list = new List<ConditionalRegistrationLog.ConditionalRegistrationHitMetadata>();
						}
						list.AddRange(hitsMetadataForUser);
					}
				}
			}
			return list;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0002431C File Offset: 0x0002251C
		public static List<ConditionalRegistrationLog.ConditionalRegistrationHitMetadata> GetHitsMetadataForUser(string loggingDirectory, string userIdentity)
		{
			List<ConditionalRegistrationLog.ConditionalRegistrationHitMetadata> list = null;
			DirectoryInfo directoryInfo = new DirectoryInfo(loggingDirectory);
			if (directoryInfo.Exists)
			{
				DirectoryInfo[] directories = directoryInfo.GetDirectories();
				foreach (DirectoryInfo directoryInfo2 in directories)
				{
					if (list == null)
					{
						list = new List<ConditionalRegistrationLog.ConditionalRegistrationHitMetadata>();
					}
					list.Add(new ConditionalRegistrationLog.ConditionalRegistrationHitMetadata(directoryInfo2.Name, userIdentity));
				}
			}
			return list;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0002437C File Offset: 0x0002257C
		public static ConditionalRegistrationLog.ConditionalRegistrationHitMetadata GetHitsForCookie(string userIdentity, string cookie)
		{
			string path = Path.Combine(ConditionalRegistrationLog.LoggingDirectory, userIdentity, cookie);
			if (Directory.Exists(path))
			{
				return new ConditionalRegistrationLog.ConditionalRegistrationHitMetadata(cookie, userIdentity);
			}
			return null;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000243A8 File Offset: 0x000225A8
		public static string GetFullPathForCookie(string userIdentity, string cookie)
		{
			return Path.Combine(ConditionalRegistrationLog.GetOrCreateLogFilePathForCookie(userIdentity, cookie, true), string.Format("{0}_{1}.xml", DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss.fff"), Thread.CurrentThread.ManagedThreadId));
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000243F0 File Offset: 0x000225F0
		public static string GetConditionalRegistrationsDirectory()
		{
			string text = Path.Combine(ConditionalRegistrationLog.LoggingDirectory, "ConditionalRegistrations");
			string result;
			try
			{
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				result = text;
			}
			catch (Exception arg)
			{
				ExTraceGlobals.DiagnosticHandlersTracer.TraceError<string, Exception>(0L, "[ConditionalRegistrationLog.GetConditionalRegistrationsDirectory] Could not create folder '{0}' due to exception: {1}", text, arg);
				result = null;
			}
			return result;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0002444C File Offset: 0x0002264C
		private static void CreateLogFilePath()
		{
			try
			{
				if (!Directory.Exists(ConditionalRegistrationLog.LoggingDirectory))
				{
					Directory.CreateDirectory(ConditionalRegistrationLog.LoggingDirectory);
				}
			}
			catch (Exception arg)
			{
				ExTraceGlobals.DiagnosticHandlersTracer.TraceError<string, Exception>(0L, "[ConditionalRegistrationLog.CreateLogFilePath] Could not create folder '{0}' due to exception: {1}", ConditionalRegistrationLog.LoggingDirectory, arg);
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0002449C File Offset: 0x0002269C
		public static string GetOrCreateLogFilePathForCookie(string userIdentity, string cookie, bool create)
		{
			string text = Path.Combine(ConditionalRegistrationLog.LoggingDirectory, userIdentity, cookie);
			if (create)
			{
				try
				{
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
				}
				catch (Exception arg)
				{
					ExTraceGlobals.DiagnosticHandlersTracer.TraceError<string, Exception>(0L, "[ConditionalRegistrationLog.CreateLogFilePathForCookie] Could not create folder '{0}' due to exception: {1}", text, arg);
					return text;
				}
				return text;
			}
			return text;
		}

		// Token: 0x0400043A RID: 1082
		private const string ConditionalRegistrationDirectory = "ConditionalRegistrations";

		// Token: 0x0400043B RID: 1083
		private static readonly string LoggingRootPath;

		// Token: 0x0400043C RID: 1084
		private static string LoggingDirectory;

		// Token: 0x0400043D RID: 1085
		private static readonly XmlWriterSettings writerSettings;

		// Token: 0x0400043E RID: 1086
		private static readonly XmlReaderSettings readerSettings;

		// Token: 0x0400043F RID: 1087
		private static string protocolName;

		// Token: 0x04000440 RID: 1088
		private static IntAppSettingsEntry MaxFiles = new IntAppSettingsEntry("MaxDiagnosticFiles", 100, ExTraceGlobals.DiagnosticHandlersTracer);

		// Token: 0x04000441 RID: 1089
		public static readonly ExEventLog EventLog = new ExEventLog(ExTraceGlobals.DiagnosticHandlersTracer.Category, "MSExchangeApplicationLogic");

		// Token: 0x020000D4 RID: 212
		public class ConditionalRegistrationHitMetadata
		{
			// Token: 0x0600091F RID: 2335 RVA: 0x00024500 File Offset: 0x00022700
			public ConditionalRegistrationHitMetadata(string cookie, string user)
			{
				this.Cookie = cookie;
				this.HitFiles = ConditionalRegistrationLog.ConditionalRegistrationHitMetadata.GetHits(cookie, user);
			}

			// Token: 0x1700024F RID: 591
			// (get) Token: 0x06000920 RID: 2336 RVA: 0x0002451C File Offset: 0x0002271C
			// (set) Token: 0x06000921 RID: 2337 RVA: 0x00024524 File Offset: 0x00022724
			public string Cookie { get; private set; }

			// Token: 0x17000250 RID: 592
			// (get) Token: 0x06000922 RID: 2338 RVA: 0x0002452D File Offset: 0x0002272D
			// (set) Token: 0x06000923 RID: 2339 RVA: 0x00024535 File Offset: 0x00022735
			public FileInfo[] HitFiles { get; private set; }

			// Token: 0x06000924 RID: 2340 RVA: 0x00024540 File Offset: 0x00022740
			private static FileInfo[] GetHits(string cookie, string user)
			{
				string orCreateLogFilePathForCookie = ConditionalRegistrationLog.GetOrCreateLogFilePathForCookie(user, cookie, false);
				DirectoryInfo directoryInfo = new DirectoryInfo(orCreateLogFilePathForCookie);
				if (!directoryInfo.Exists)
				{
					return null;
				}
				return directoryInfo.GetFiles("*.xml", SearchOption.TopDirectoryOnly);
			}
		}
	}
}
