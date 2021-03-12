using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006BE RID: 1726
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class Utils
	{
		// Token: 0x06004566 RID: 17766 RVA: 0x00127561 File Offset: 0x00125761
		internal static WindowsImpersonationContext ImpersonateUser(WindowsIdentity identity)
		{
			ExtensionToContentTypeMapper.Instance.GetContentTypeByExtension("txt");
			return identity.Impersonate();
		}

		// Token: 0x06004567 RID: 17767 RVA: 0x0012757C File Offset: 0x0012577C
		internal static bool IsValidUncUri(Uri uri)
		{
			if (uri.LocalPath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
			{
				return false;
			}
			foreach (string text in uri.Segments)
			{
				if (text.TrimEnd(new char[]
				{
					'\\',
					'/'
				}).IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004568 RID: 17768 RVA: 0x001275E8 File Offset: 0x001257E8
		internal static void UndoContext(ref WindowsImpersonationContext context)
		{
			WindowsImpersonationContext windowsImpersonationContext = context;
			context = null;
			if (windowsImpersonationContext != null)
			{
				windowsImpersonationContext.Undo();
				windowsImpersonationContext.Dispose();
			}
		}

		// Token: 0x06004569 RID: 17769 RVA: 0x0012760C File Offset: 0x0012580C
		internal static G DoSharepointTask<G>(WindowsIdentity identity, ObjectId itemToAccess, SharepointSiteId sharepointId, bool objectOpened, Utils.MethodType methodType, Utils.DoTask<G> doTask)
		{
			WindowsImpersonationContext windowsImpersonationContext = null;
			G result;
			try
			{
				windowsImpersonationContext = Utils.ImpersonateUser(identity);
				result = doTask();
			}
			catch (ObjectNotFoundException innerException)
			{
				Utils.UndoContext(ref windowsImpersonationContext);
				if (objectOpened)
				{
					throw new ObjectMovedOrDeletedException(itemToAccess, itemToAccess.ToString(), innerException);
				}
				throw;
			}
			catch (AccessDeniedException innerException2)
			{
				Utils.UndoContext(ref windowsImpersonationContext);
				if (methodType == Utils.MethodType.GetView)
				{
					throw new GetViewAccessDeniedException(itemToAccess, Strings.ExAccessDeniedForGetViewUnder((itemToAccess != null) ? itemToAccess.ToString() : null), innerException2);
				}
				if (methodType == Utils.MethodType.GetStream)
				{
					throw new DocumentStreamAccessDeniedException(itemToAccess, Strings.ExAccessDenied((itemToAccess != null) ? itemToAccess.ToString() : null), innerException2);
				}
				throw new AccessDeniedException(itemToAccess, Strings.ExAccessDenied((itemToAccess != null) ? itemToAccess.ToString() : null), innerException2);
			}
			catch (SoapException ex)
			{
				Utils.UndoContext(ref windowsImpersonationContext);
				throw Utils.TranslateException(ex, sharepointId);
			}
			catch (WebException ex2)
			{
				Utils.UndoContext(ref windowsImpersonationContext);
				throw Utils.TranslateException(ex2, sharepointId, objectOpened, methodType);
			}
			catch
			{
				Utils.UndoContext(ref windowsImpersonationContext);
				throw;
			}
			finally
			{
				Utils.UndoContext(ref windowsImpersonationContext);
			}
			return result;
		}

		// Token: 0x0600456A RID: 17770 RVA: 0x00127730 File Offset: 0x00125930
		internal static G DoUncTask<G>(WindowsIdentity identity, UncObjectId uncObjectId, bool objectOpened, Utils.MethodType methodType, Utils.DoTask<G> doTask)
		{
			WindowsImpersonationContext windowsImpersonationContext = Utils.ImpersonateUser(identity);
			G result;
			try
			{
				result = doTask();
			}
			catch (IOException ex)
			{
				Utils.UndoContext(ref windowsImpersonationContext);
				throw Utils.TranslateException(ex, uncObjectId, objectOpened);
			}
			catch (UnauthorizedAccessException innerException)
			{
				Utils.UndoContext(ref windowsImpersonationContext);
				if (methodType == Utils.MethodType.GetView)
				{
					throw new GetViewAccessDeniedException(uncObjectId, Strings.ExAccessDeniedForGetViewUnder((uncObjectId != null) ? uncObjectId.Path.LocalPath : null), innerException);
				}
				if (methodType == Utils.MethodType.GetStream)
				{
					throw new DocumentStreamAccessDeniedException(uncObjectId, Strings.ExAccessDenied((uncObjectId != null) ? uncObjectId.Path.LocalPath : null), innerException);
				}
				throw new AccessDeniedException(uncObjectId, Strings.ExAccessDenied((uncObjectId != null) ? uncObjectId.Path.LocalPath : null), innerException);
			}
			catch (SecurityException innerException2)
			{
				Utils.UndoContext(ref windowsImpersonationContext);
				if (methodType == Utils.MethodType.GetView)
				{
					throw new GetViewAccessDeniedException(uncObjectId, Strings.ExAccessDeniedForGetViewUnder((uncObjectId != null) ? uncObjectId.Path.LocalPath : null), innerException2);
				}
				if (methodType == Utils.MethodType.GetStream)
				{
					throw new DocumentStreamAccessDeniedException(uncObjectId, Strings.ExAccessDeniedForGetViewUnder((uncObjectId != null) ? uncObjectId.Path.LocalPath : null), innerException2);
				}
				throw new AccessDeniedException(uncObjectId, Strings.ExAccessDenied((uncObjectId != null) ? uncObjectId.Path.LocalPath : null), innerException2);
			}
			catch
			{
				Utils.UndoContext(ref windowsImpersonationContext);
				throw;
			}
			finally
			{
				Utils.UndoContext(ref windowsImpersonationContext);
			}
			return result;
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x0012788C File Offset: 0x00125A8C
		internal static DocumentLibraryException TranslateException(WebException ex, SharepointSiteId objectId, bool objectOpened, Utils.MethodType methodType)
		{
			if (ex.Response != null)
			{
				HttpStatusCode statusCode = ((HttpWebResponse)ex.Response).StatusCode;
				HttpStatusCode httpStatusCode = statusCode;
				if (httpStatusCode <= HttpStatusCode.NotFound)
				{
					if (httpStatusCode != HttpStatusCode.Unauthorized)
					{
						if (httpStatusCode == HttpStatusCode.NotFound)
						{
							if (objectOpened)
							{
								return new ObjectMovedOrDeletedException(objectId, "ObjectNotFound", ex);
							}
							return new ObjectNotFoundException(objectId, "ObjectNotFound", ex);
						}
					}
					else
					{
						if (methodType == Utils.MethodType.GetView)
						{
							throw new GetViewAccessDeniedException(objectId, Strings.ExAccessDeniedForGetViewUnder((objectId != null) ? objectId.ToString() : null), ex);
						}
						if (methodType == Utils.MethodType.GetStream)
						{
							throw new DocumentStreamAccessDeniedException(objectId, Strings.ExAccessDenied((objectId != null) ? objectId.ToString() : null), ex);
						}
						throw new AccessDeniedException(objectId, Strings.ExAccessDenied((objectId != null) ? objectId.ToString() : null), ex);
					}
				}
				else if (httpStatusCode == HttpStatusCode.ProxyAuthenticationRequired || httpStatusCode == HttpStatusCode.BadGateway)
				{
					return new ProxyConnectionException(objectId, Strings.ExProxyConnectionFailure, ex);
				}
				return new ConnectionException(objectId, Strings.ExConnectionFailure, ex);
			}
			switch (ex.Status)
			{
			case WebExceptionStatus.NameResolutionFailure:
				if (objectOpened)
				{
					return new ObjectMovedOrDeletedException(objectId, "ObjectNotFound", ex);
				}
				return new ObjectNotFoundException(objectId, "ObjectNotFound", ex);
			case WebExceptionStatus.ConnectFailure:
			case WebExceptionStatus.ReceiveFailure:
			case WebExceptionStatus.SendFailure:
			case WebExceptionStatus.PipelineFailure:
			case WebExceptionStatus.RequestCanceled:
			case WebExceptionStatus.ProtocolError:
			case WebExceptionStatus.ConnectionClosed:
			case WebExceptionStatus.TrustFailure:
			case WebExceptionStatus.SecureChannelFailure:
			case WebExceptionStatus.ServerProtocolViolation:
			case WebExceptionStatus.KeepAliveFailure:
			case WebExceptionStatus.Pending:
			case WebExceptionStatus.Timeout:
			case WebExceptionStatus.MessageLengthLimitExceeded:
			case WebExceptionStatus.CacheEntryNotFound:
			case WebExceptionStatus.RequestProhibitedByCachePolicy:
				return new ConnectionException(objectId, Strings.ExConnectionFailure, ex);
			case WebExceptionStatus.ProxyNameResolutionFailure:
			case WebExceptionStatus.RequestProhibitedByProxy:
				return new ProxyConnectionException(objectId, Strings.ExProxyConnectionFailure, ex);
			case WebExceptionStatus.UnknownError:
				return new UnknownErrorException(objectId, Strings.ExUnknownError, ex);
			default:
				return new UnknownErrorException(objectId, Strings.ExUnknownError, ex);
			}
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x00127A21 File Offset: 0x00125C21
		internal static DocumentLibraryException TranslateException(SoapException ex, SharepointSiteId objectId)
		{
			return new UnknownErrorException(objectId, Strings.ExUnknownError, ex);
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x00127A30 File Offset: 0x00125C30
		internal static DocumentLibraryException TranslateException(IOException ex, UncObjectId objectId, bool objectOpened)
		{
			if (ex is FileNotFoundException || ex is DirectoryNotFoundException)
			{
				if (objectOpened)
				{
					return new ObjectMovedOrDeletedException(objectId, Strings.ExObjectNotFound((objectId != null) ? objectId.Path.LocalPath : null), ex);
				}
				return new ObjectNotFoundException(objectId, Strings.ExObjectNotFound((objectId != null) ? objectId.Path.LocalPath : null), ex);
			}
			else
			{
				if (ex is PathTooLongException)
				{
					return new PathTooLongException(objectId, Strings.ExPathTooLong((objectId != null) ? objectId.Path.LocalPath : null), ex);
				}
				return new UnknownErrorException(objectId, Strings.ExUnknownError, ex);
			}
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x00127AC0 File Offset: 0x00125CC0
		internal static int CompareValues(object left, object right)
		{
			Type left2 = (left != null) ? left.GetType() : null;
			Type type = (right != null) ? right.GetType() : null;
			if (left2 == typeof(PropertyError))
			{
				if (type == typeof(PropertyError))
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (type == typeof(PropertyError))
				{
					return -1;
				}
				if (left == null)
				{
					if (right != null)
					{
						return -1;
					}
					return 0;
				}
				else
				{
					if (right == null)
					{
						return 1;
					}
					if (!(left2 == type))
					{
						throw new ArgumentException("left & right not of same type");
					}
					IComparable comparable = left as IComparable;
					if (comparable != null)
					{
						return comparable.CompareTo(right);
					}
					throw new ArgumentException("arguments not IComparable");
				}
			}
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x00127B62 File Offset: 0x00125D62
		[Conditional("DEBUG")]
		internal static void Assert(bool condition, string message, params object[] messageArgs)
		{
		}

		// Token: 0x04002608 RID: 9736
		internal static int GetViewMaxRows = 10000;

		// Token: 0x04002609 RID: 9737
		internal static int MaxFilterDepth = 6;

		// Token: 0x020006BF RID: 1727
		internal enum MethodType
		{
			// Token: 0x0400260B RID: 9739
			Read = 1,
			// Token: 0x0400260C RID: 9740
			GetView,
			// Token: 0x0400260D RID: 9741
			GetStream
		}

		// Token: 0x020006C0 RID: 1728
		// (Invoke) Token: 0x06004573 RID: 17779
		internal delegate G DoTask<G>();
	}
}
