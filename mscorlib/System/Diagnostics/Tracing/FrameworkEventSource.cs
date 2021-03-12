using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000407 RID: 1031
	[FriendAccessAllowed]
	[EventSource(Guid = "8E9F5090-2D75-4d03-8A81-E5AFBF85DAF1", Name = "System.Diagnostics.Eventing.FrameworkEventSource")]
	internal sealed class FrameworkEventSource : EventSource
	{
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000CC782 File Offset: 0x000CA982
		public static bool IsInitialized
		{
			get
			{
				return FrameworkEventSource.Log != null;
			}
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x000CC78C File Offset: 0x000CA98C
		private FrameworkEventSource() : base(new Guid(2392805520U, 11637, 19715, 138, 129, 229, 175, 191, 133, 218, 241), "System.Diagnostics.Eventing.FrameworkEventSource")
		{
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000CC7E0 File Offset: 0x000CA9E0
		[NonEvent]
		[SecuritySafeCritical]
		private unsafe void WriteEvent(int eventId, long arg1, int arg2, string arg3, bool arg4)
		{
			if (base.IsEnabled())
			{
				if (arg3 == null)
				{
					arg3 = "";
				}
				fixed (string text = arg3)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData))];
					ptr2->DataPointer = (IntPtr)((void*)(&arg1));
					ptr2->Size = 8;
					ptr2[1].DataPointer = (IntPtr)((void*)(&arg2));
					ptr2[1].Size = 4;
					ptr2[2].DataPointer = (IntPtr)((void*)ptr);
					ptr2[2].Size = (arg3.Length + 1) * 2;
					ptr2[3].DataPointer = (IntPtr)((void*)(&arg4));
					ptr2[3].Size = 4;
					base.WriteEventCore(eventId, 4, ptr2);
				}
			}
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000CC8C0 File Offset: 0x000CAAC0
		[NonEvent]
		[SecuritySafeCritical]
		private unsafe void WriteEvent(int eventId, long arg1, int arg2, string arg3)
		{
			if (base.IsEnabled())
			{
				if (arg3 == null)
				{
					arg3 = "";
				}
				fixed (string text = arg3)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData))];
					ptr2->DataPointer = (IntPtr)((void*)(&arg1));
					ptr2->Size = 8;
					ptr2[1].DataPointer = (IntPtr)((void*)(&arg2));
					ptr2[1].Size = 4;
					ptr2[2].DataPointer = (IntPtr)((void*)ptr);
					ptr2[2].Size = (arg3.Length + 1) * 2;
					base.WriteEventCore(eventId, 3, ptr2);
				}
			}
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000CC974 File Offset: 0x000CAB74
		[NonEvent]
		[SecuritySafeCritical]
		private unsafe void WriteEvent(int eventId, long arg1, string arg2, bool arg3, bool arg4)
		{
			if (base.IsEnabled())
			{
				if (arg2 == null)
				{
					arg2 = "";
				}
				fixed (string text = arg2)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData))];
					ptr2->DataPointer = (IntPtr)((void*)(&arg1));
					ptr2->Size = 8;
					ptr2[1].DataPointer = (IntPtr)((void*)ptr);
					ptr2[1].Size = (arg2.Length + 1) * 2;
					ptr2[2].DataPointer = (IntPtr)((void*)(&arg3));
					ptr2[2].Size = 4;
					ptr2[3].DataPointer = (IntPtr)((void*)(&arg4));
					ptr2[3].Size = 4;
					base.WriteEventCore(eventId, 4, ptr2);
				}
			}
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000CCA50 File Offset: 0x000CAC50
		[NonEvent]
		[SecuritySafeCritical]
		private unsafe void WriteEvent(int eventId, long arg1, bool arg2, bool arg3)
		{
			if (base.IsEnabled())
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&arg3));
				ptr[2].Size = 4;
				base.WriteEventCore(eventId, 3, ptr);
			}
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000CCADC File Offset: 0x000CACDC
		[NonEvent]
		[SecuritySafeCritical]
		private unsafe void WriteEvent(int eventId, long arg1, bool arg2, bool arg3, int arg4)
		{
			if (base.IsEnabled())
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&arg3));
				ptr[2].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&arg4));
				ptr[3].Size = 4;
				base.WriteEventCore(eventId, 4, ptr);
			}
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000CCB93 File Offset: 0x000CAD93
		[Event(1, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerLookupStarted(string baseName, string mainAssemblyName, string cultureName)
		{
			base.WriteEvent(1, baseName, mainAssemblyName, cultureName);
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000CCB9F File Offset: 0x000CAD9F
		[Event(2, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerLookingForResourceSet(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(2, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000CCBB3 File Offset: 0x000CADB3
		[Event(3, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerFoundResourceSetInCache(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(3, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000CCBC7 File Offset: 0x000CADC7
		[Event(4, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		public void ResourceManagerFoundResourceSetInCacheUnexpected(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(4, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000CCBDB File Offset: 0x000CADDB
		[Event(5, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerStreamFound(string baseName, string mainAssemblyName, string cultureName, string loadedAssemblyName, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(5, new object[]
				{
					baseName,
					mainAssemblyName,
					cultureName,
					loadedAssemblyName,
					resourceFileName
				});
			}
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000CCC08 File Offset: 0x000CAE08
		[Event(6, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		public void ResourceManagerStreamNotFound(string baseName, string mainAssemblyName, string cultureName, string loadedAssemblyName, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(6, new object[]
				{
					baseName,
					mainAssemblyName,
					cultureName,
					loadedAssemblyName,
					resourceFileName
				});
			}
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x000CCC35 File Offset: 0x000CAE35
		[Event(7, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerGetSatelliteAssemblySucceeded(string baseName, string mainAssemblyName, string cultureName, string assemblyName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(7, new object[]
				{
					baseName,
					mainAssemblyName,
					cultureName,
					assemblyName
				});
			}
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000CCC5D File Offset: 0x000CAE5D
		[Event(8, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		public void ResourceManagerGetSatelliteAssemblyFailed(string baseName, string mainAssemblyName, string cultureName, string assemblyName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(8, new object[]
				{
					baseName,
					mainAssemblyName,
					cultureName,
					assemblyName
				});
			}
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000CCC85 File Offset: 0x000CAE85
		[Event(9, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerCaseInsensitiveResourceStreamLookupSucceeded(string baseName, string mainAssemblyName, string assemblyName, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(9, new object[]
				{
					baseName,
					mainAssemblyName,
					assemblyName,
					resourceFileName
				});
			}
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000CCCAE File Offset: 0x000CAEAE
		[Event(10, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		public void ResourceManagerCaseInsensitiveResourceStreamLookupFailed(string baseName, string mainAssemblyName, string assemblyName, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(10, new object[]
				{
					baseName,
					mainAssemblyName,
					assemblyName,
					resourceFileName
				});
			}
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000CCCD7 File Offset: 0x000CAED7
		[Event(11, Level = EventLevel.Error, Keywords = (EventKeywords)1L)]
		public void ResourceManagerManifestResourceAccessDenied(string baseName, string mainAssemblyName, string assemblyName, string canonicalName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(11, new object[]
				{
					baseName,
					mainAssemblyName,
					assemblyName,
					canonicalName
				});
			}
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000CCD00 File Offset: 0x000CAF00
		[Event(12, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerNeutralResourcesSufficient(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(12, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x000CCD15 File Offset: 0x000CAF15
		[Event(13, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		public void ResourceManagerNeutralResourceAttributeMissing(string mainAssemblyName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(13, mainAssemblyName);
			}
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000CCD28 File Offset: 0x000CAF28
		[Event(14, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerCreatingResourceSet(string baseName, string mainAssemblyName, string cultureName, string fileName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(14, new object[]
				{
					baseName,
					mainAssemblyName,
					cultureName,
					fileName
				});
			}
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x000CCD51 File Offset: 0x000CAF51
		[Event(15, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerNotCreatingResourceSet(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(15, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000CCD66 File Offset: 0x000CAF66
		[Event(16, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		public void ResourceManagerLookupFailed(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(16, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000CCD7B File Offset: 0x000CAF7B
		[Event(17, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerReleasingResources(string baseName, string mainAssemblyName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(17, baseName, mainAssemblyName);
			}
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000CCD8F File Offset: 0x000CAF8F
		[Event(18, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		public void ResourceManagerNeutralResourcesNotFound(string baseName, string mainAssemblyName, string resName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(18, baseName, mainAssemblyName, resName);
			}
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000CCDA4 File Offset: 0x000CAFA4
		[Event(19, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerNeutralResourcesFound(string baseName, string mainAssemblyName, string resName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(19, baseName, mainAssemblyName, resName);
			}
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000CCDB9 File Offset: 0x000CAFB9
		[Event(20, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerAddingCultureFromConfigFile(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(20, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000CCDCE File Offset: 0x000CAFCE
		[Event(21, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerCultureNotFoundInConfigFile(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(21, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000CCDE3 File Offset: 0x000CAFE3
		[Event(22, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerCultureFoundInConfigFile(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(22, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000CCDF8 File Offset: 0x000CAFF8
		[NonEvent]
		public void ResourceManagerLookupStarted(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerLookupStarted(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000CCE10 File Offset: 0x000CB010
		[NonEvent]
		public void ResourceManagerLookingForResourceSet(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerLookingForResourceSet(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000CCE28 File Offset: 0x000CB028
		[NonEvent]
		public void ResourceManagerFoundResourceSetInCache(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerFoundResourceSetInCache(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000CCE40 File Offset: 0x000CB040
		[NonEvent]
		public void ResourceManagerFoundResourceSetInCacheUnexpected(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerFoundResourceSetInCacheUnexpected(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000CCE58 File Offset: 0x000CB058
		[NonEvent]
		public void ResourceManagerStreamFound(string baseName, Assembly mainAssembly, string cultureName, Assembly loadedAssembly, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerStreamFound(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, FrameworkEventSource.GetName(loadedAssembly), resourceFileName);
			}
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x000CCE79 File Offset: 0x000CB079
		[NonEvent]
		public void ResourceManagerStreamNotFound(string baseName, Assembly mainAssembly, string cultureName, Assembly loadedAssembly, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerStreamNotFound(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, FrameworkEventSource.GetName(loadedAssembly), resourceFileName);
			}
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x000CCE9A File Offset: 0x000CB09A
		[NonEvent]
		public void ResourceManagerGetSatelliteAssemblySucceeded(string baseName, Assembly mainAssembly, string cultureName, string assemblyName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerGetSatelliteAssemblySucceeded(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, assemblyName);
			}
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x000CCEB4 File Offset: 0x000CB0B4
		[NonEvent]
		public void ResourceManagerGetSatelliteAssemblyFailed(string baseName, Assembly mainAssembly, string cultureName, string assemblyName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerGetSatelliteAssemblyFailed(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, assemblyName);
			}
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x000CCECE File Offset: 0x000CB0CE
		[NonEvent]
		public void ResourceManagerCaseInsensitiveResourceStreamLookupSucceeded(string baseName, Assembly mainAssembly, string assemblyName, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerCaseInsensitiveResourceStreamLookupSucceeded(baseName, FrameworkEventSource.GetName(mainAssembly), assemblyName, resourceFileName);
			}
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x000CCEE8 File Offset: 0x000CB0E8
		[NonEvent]
		public void ResourceManagerCaseInsensitiveResourceStreamLookupFailed(string baseName, Assembly mainAssembly, string assemblyName, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerCaseInsensitiveResourceStreamLookupFailed(baseName, FrameworkEventSource.GetName(mainAssembly), assemblyName, resourceFileName);
			}
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x000CCF02 File Offset: 0x000CB102
		[NonEvent]
		public void ResourceManagerManifestResourceAccessDenied(string baseName, Assembly mainAssembly, string assemblyName, string canonicalName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerManifestResourceAccessDenied(baseName, FrameworkEventSource.GetName(mainAssembly), assemblyName, canonicalName);
			}
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x000CCF1C File Offset: 0x000CB11C
		[NonEvent]
		public void ResourceManagerNeutralResourcesSufficient(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerNeutralResourcesSufficient(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000CCF34 File Offset: 0x000CB134
		[NonEvent]
		public void ResourceManagerNeutralResourceAttributeMissing(Assembly mainAssembly)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerNeutralResourceAttributeMissing(FrameworkEventSource.GetName(mainAssembly));
			}
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000CCF4A File Offset: 0x000CB14A
		[NonEvent]
		public void ResourceManagerCreatingResourceSet(string baseName, Assembly mainAssembly, string cultureName, string fileName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerCreatingResourceSet(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, fileName);
			}
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000CCF64 File Offset: 0x000CB164
		[NonEvent]
		public void ResourceManagerNotCreatingResourceSet(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerNotCreatingResourceSet(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000CCF7C File Offset: 0x000CB17C
		[NonEvent]
		public void ResourceManagerLookupFailed(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerLookupFailed(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000CCF94 File Offset: 0x000CB194
		[NonEvent]
		public void ResourceManagerReleasingResources(string baseName, Assembly mainAssembly)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerReleasingResources(baseName, FrameworkEventSource.GetName(mainAssembly));
			}
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000CCFAB File Offset: 0x000CB1AB
		[NonEvent]
		public void ResourceManagerNeutralResourcesNotFound(string baseName, Assembly mainAssembly, string resName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerNeutralResourcesNotFound(baseName, FrameworkEventSource.GetName(mainAssembly), resName);
			}
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000CCFC3 File Offset: 0x000CB1C3
		[NonEvent]
		public void ResourceManagerNeutralResourcesFound(string baseName, Assembly mainAssembly, string resName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerNeutralResourcesFound(baseName, FrameworkEventSource.GetName(mainAssembly), resName);
			}
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000CCFDB File Offset: 0x000CB1DB
		[NonEvent]
		public void ResourceManagerAddingCultureFromConfigFile(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerAddingCultureFromConfigFile(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000CCFF3 File Offset: 0x000CB1F3
		[NonEvent]
		public void ResourceManagerCultureNotFoundInConfigFile(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerCultureNotFoundInConfigFile(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000CD00B File Offset: 0x000CB20B
		[NonEvent]
		public void ResourceManagerCultureFoundInConfigFile(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerCultureFoundInConfigFile(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000CD023 File Offset: 0x000CB223
		private static string GetName(Assembly assembly)
		{
			if (assembly == null)
			{
				return "<<NULL>>";
			}
			return assembly.FullName;
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000CD03A File Offset: 0x000CB23A
		[Event(30, Level = EventLevel.Verbose, Keywords = (EventKeywords)18L)]
		public void ThreadPoolEnqueueWork(long workID)
		{
			base.WriteEvent(30, workID);
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000CD045 File Offset: 0x000CB245
		[NonEvent]
		[SecuritySafeCritical]
		public unsafe void ThreadPoolEnqueueWorkObject(object workID)
		{
			this.ThreadPoolEnqueueWork((long)((ulong)(*(IntPtr*)((void*)JitHelpers.UnsafeCastToStackPointer<object>(ref workID)))));
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000CD05B File Offset: 0x000CB25B
		[Event(31, Level = EventLevel.Verbose, Keywords = (EventKeywords)18L)]
		public void ThreadPoolDequeueWork(long workID)
		{
			base.WriteEvent(31, workID);
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000CD066 File Offset: 0x000CB266
		[NonEvent]
		[SecuritySafeCritical]
		public unsafe void ThreadPoolDequeueWorkObject(object workID)
		{
			this.ThreadPoolDequeueWork((long)((ulong)(*(IntPtr*)((void*)JitHelpers.UnsafeCastToStackPointer<object>(ref workID)))));
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000CD07C File Offset: 0x000CB27C
		[Event(140, Level = EventLevel.Informational, Keywords = (EventKeywords)4L, ActivityOptions = EventActivityOptions.Disable, Task = (EventTask)1, Opcode = EventOpcode.Start, Version = 1)]
		private void GetResponseStart(long id, string uri, bool success, bool synchronous)
		{
			this.WriteEvent(140, id, uri, success, synchronous);
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000CD08E File Offset: 0x000CB28E
		[Event(141, Level = EventLevel.Informational, Keywords = (EventKeywords)4L, ActivityOptions = EventActivityOptions.Disable, Task = (EventTask)1, Opcode = EventOpcode.Stop, Version = 1)]
		private void GetResponseStop(long id, bool success, bool synchronous, int statusCode)
		{
			this.WriteEvent(141, id, success, synchronous, statusCode);
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000CD0A0 File Offset: 0x000CB2A0
		[Event(142, Level = EventLevel.Informational, Keywords = (EventKeywords)4L, ActivityOptions = EventActivityOptions.Disable, Task = (EventTask)2, Opcode = EventOpcode.Start, Version = 1)]
		private void GetRequestStreamStart(long id, string uri, bool success, bool synchronous)
		{
			this.WriteEvent(142, id, uri, success, synchronous);
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000CD0B2 File Offset: 0x000CB2B2
		[Event(143, Level = EventLevel.Informational, Keywords = (EventKeywords)4L, ActivityOptions = EventActivityOptions.Disable, Task = (EventTask)2, Opcode = EventOpcode.Stop, Version = 1)]
		private void GetRequestStreamStop(long id, bool success, bool synchronous)
		{
			this.WriteEvent(143, id, success, synchronous);
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000CD0C2 File Offset: 0x000CB2C2
		[NonEvent]
		[SecuritySafeCritical]
		public void BeginGetResponse(object id, string uri, bool success, bool synchronous)
		{
			if (base.IsEnabled())
			{
				this.GetResponseStart(FrameworkEventSource.IdForObject(id), uri, success, synchronous);
			}
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000CD0DC File Offset: 0x000CB2DC
		[NonEvent]
		[SecuritySafeCritical]
		public void EndGetResponse(object id, bool success, bool synchronous, int statusCode)
		{
			if (base.IsEnabled())
			{
				this.GetResponseStop(FrameworkEventSource.IdForObject(id), success, synchronous, statusCode);
			}
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000CD0F6 File Offset: 0x000CB2F6
		[NonEvent]
		[SecuritySafeCritical]
		public void BeginGetRequestStream(object id, string uri, bool success, bool synchronous)
		{
			if (base.IsEnabled())
			{
				this.GetRequestStreamStart(FrameworkEventSource.IdForObject(id), uri, success, synchronous);
			}
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000CD110 File Offset: 0x000CB310
		[NonEvent]
		[SecuritySafeCritical]
		public void EndGetRequestStream(object id, bool success, bool synchronous)
		{
			if (base.IsEnabled())
			{
				this.GetRequestStreamStop(FrameworkEventSource.IdForObject(id), success, synchronous);
			}
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000CD128 File Offset: 0x000CB328
		[Event(150, Level = EventLevel.Informational, Keywords = (EventKeywords)16L, Task = (EventTask)3, Opcode = EventOpcode.Send)]
		public void ThreadTransferSend(long id, int kind, string info, bool multiDequeues)
		{
			if (base.IsEnabled())
			{
				this.WriteEvent(150, id, kind, info, multiDequeues);
			}
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x000CD142 File Offset: 0x000CB342
		[NonEvent]
		[SecuritySafeCritical]
		public unsafe void ThreadTransferSendObj(object id, int kind, string info, bool multiDequeues)
		{
			this.ThreadTransferSend((long)((ulong)(*(IntPtr*)((void*)JitHelpers.UnsafeCastToStackPointer<object>(ref id)))), kind, info, multiDequeues);
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x000CD15C File Offset: 0x000CB35C
		[Event(151, Level = EventLevel.Informational, Keywords = (EventKeywords)16L, Task = (EventTask)3, Opcode = EventOpcode.Receive)]
		public void ThreadTransferReceive(long id, int kind, string info)
		{
			if (base.IsEnabled())
			{
				this.WriteEvent(151, id, kind, info);
			}
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x000CD174 File Offset: 0x000CB374
		[NonEvent]
		[SecuritySafeCritical]
		public unsafe void ThreadTransferReceiveObj(object id, int kind, string info)
		{
			this.ThreadTransferReceive((long)((ulong)(*(IntPtr*)((void*)JitHelpers.UnsafeCastToStackPointer<object>(ref id)))), kind, info);
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000CD18C File Offset: 0x000CB38C
		[Event(152, Level = EventLevel.Informational, Keywords = (EventKeywords)16L, Task = (EventTask)3, Opcode = (EventOpcode)11)]
		public void ThreadTransferReceiveHandled(long id, int kind, string info)
		{
			if (base.IsEnabled())
			{
				this.WriteEvent(152, id, kind, info);
			}
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000CD1A4 File Offset: 0x000CB3A4
		[NonEvent]
		[SecuritySafeCritical]
		public unsafe void ThreadTransferReceiveHandledObj(object id, int kind, string info)
		{
			this.ThreadTransferReceive((long)((ulong)(*(IntPtr*)((void*)JitHelpers.UnsafeCastToStackPointer<object>(ref id)))), kind, info);
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000CD1BC File Offset: 0x000CB3BC
		private static long IdForObject(object obj)
		{
			return (long)obj.GetHashCode() + 9223372032559808512L;
		}

		// Token: 0x0400172B RID: 5931
		public static readonly FrameworkEventSource Log = new FrameworkEventSource();

		// Token: 0x02000B60 RID: 2912
		public static class Keywords
		{
			// Token: 0x04003437 RID: 13367
			public const EventKeywords Loader = (EventKeywords)1L;

			// Token: 0x04003438 RID: 13368
			public const EventKeywords ThreadPool = (EventKeywords)2L;

			// Token: 0x04003439 RID: 13369
			public const EventKeywords NetClient = (EventKeywords)4L;

			// Token: 0x0400343A RID: 13370
			public const EventKeywords DynamicTypeUsage = (EventKeywords)8L;

			// Token: 0x0400343B RID: 13371
			public const EventKeywords ThreadTransfer = (EventKeywords)16L;
		}

		// Token: 0x02000B61 RID: 2913
		[FriendAccessAllowed]
		public static class Tasks
		{
			// Token: 0x0400343C RID: 13372
			public const EventTask GetResponse = (EventTask)1;

			// Token: 0x0400343D RID: 13373
			public const EventTask GetRequestStream = (EventTask)2;

			// Token: 0x0400343E RID: 13374
			public const EventTask ThreadTransfer = (EventTask)3;
		}

		// Token: 0x02000B62 RID: 2914
		[FriendAccessAllowed]
		public static class Opcodes
		{
			// Token: 0x0400343F RID: 13375
			public const EventOpcode ReceiveHandled = (EventOpcode)11;
		}
	}
}
