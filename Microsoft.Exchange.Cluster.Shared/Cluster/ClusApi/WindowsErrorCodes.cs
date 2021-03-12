using System;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000060 RID: 96
	internal static class WindowsErrorCodes
	{
		// Token: 0x04000170 RID: 368
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x04000171 RID: 369
		internal const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x04000172 RID: 370
		internal const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x04000173 RID: 371
		internal const int ERROR_TOO_MANY_OPEN_FILES = 4;

		// Token: 0x04000174 RID: 372
		internal const uint ServerManagerInvalidArgs = 4U;

		// Token: 0x04000175 RID: 373
		internal const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x04000176 RID: 374
		internal const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x04000177 RID: 375
		internal const int ERROR_INVALID_DRIVE = 15;

		// Token: 0x04000178 RID: 376
		internal const int ERROR_BAD_UNIT = 20;

		// Token: 0x04000179 RID: 377
		internal const int ERROR_NOT_READY = 21;

		// Token: 0x0400017A RID: 378
		internal const int ERROR_CRC = 23;

		// Token: 0x0400017B RID: 379
		internal const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x0400017C RID: 380
		internal const int ERROR_HANDLE_EOF = 38;

		// Token: 0x0400017D RID: 381
		internal const int ERROR_HANDLE_DISK_FULL = 39;

		// Token: 0x0400017E RID: 382
		internal const int ERROR_BAD_NETPATH = 53;

		// Token: 0x0400017F RID: 383
		internal const uint ERROR_BAD_NET_NAME = 67U;

		// Token: 0x04000180 RID: 384
		internal const int ERROR_SHARING_PAUSED = 70;

		// Token: 0x04000181 RID: 385
		internal const int ERROR_FILE_EXISTS = 80;

		// Token: 0x04000182 RID: 386
		internal const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x04000183 RID: 387
		internal const int ERROR_DISK_FULL = 112;

		// Token: 0x04000184 RID: 388
		internal const uint ERROR_INVALID_LEVEL = 124U;

		// Token: 0x04000185 RID: 389
		internal const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x04000186 RID: 390
		internal const int ERROR_FILENAME_EXCED_RANGE = 206;

		// Token: 0x04000187 RID: 391
		internal const int ERROR_MORE_DATA = 234;

		// Token: 0x04000188 RID: 392
		internal const int WAIT_TIMEOUT = 258;

		// Token: 0x04000189 RID: 393
		internal const int ERROR_NO_MORE_ITEMS = 259;

		// Token: 0x0400018A RID: 394
		internal const int ERROR_OPERATION_ABORTED = 995;

		// Token: 0x0400018B RID: 395
		internal const uint ERROR_IO_PENDING = 997U;

		// Token: 0x0400018C RID: 396
		internal const uint ServerManagerFailed = 1000U;

		// Token: 0x0400018D RID: 397
		internal const uint ERROR_STACK_OVERFLOW = 1001U;

		// Token: 0x0400018E RID: 398
		internal const uint ServerManagerFailedRebootRequired = 1001U;

		// Token: 0x0400018F RID: 399
		internal const uint ServerManagerErrorUnrecoverable = 1002U;

		// Token: 0x04000190 RID: 400
		internal const uint ServerManagerNoChangeNeeded = 1003U;

		// Token: 0x04000191 RID: 401
		internal const uint ERROR_CAN_NOT_COMPLETE = 1003U;

		// Token: 0x04000192 RID: 402
		internal const uint ServerManagerErrorAnotherSessionDetected = 1004U;

		// Token: 0x04000193 RID: 403
		internal const uint ERROR_INVALID_FLAGS = 1004U;

		// Token: 0x04000194 RID: 404
		internal const int ERROR_KEY_DELETED = 1018;

		// Token: 0x04000195 RID: 405
		internal const int ERROR_SERVICE_DOES_NOT_EXIST = 1060;

		// Token: 0x04000196 RID: 406
		internal const int ERROR_SHUTDOWN_IN_PROGRESS = 1115;

		// Token: 0x04000197 RID: 407
		internal const int ERROR_IO_DEVICE = 1117;

		// Token: 0x04000198 RID: 408
		internal const int ERROR_FILE_CORRUPT = 1392;

		// Token: 0x04000199 RID: 409
		internal const int ERROR_DISK_CORRUPT = 1393;

		// Token: 0x0400019A RID: 410
		internal const int ERROR_NO_SYSTEM_RESOURCES = 1450;

		// Token: 0x0400019B RID: 411
		internal const int ERROR_UNRECOGNIZED_VOLUME = 1005;

		// Token: 0x0400019C RID: 412
		internal const int ERROR_TIMEOUT = 1460;

		// Token: 0x0400019D RID: 413
		internal const uint NERR_NetNameNotFound = 2310U;

		// Token: 0x0400019E RID: 414
		internal const uint ERROR_SUCCESS_REBOOT_REQUIRED = 3010U;

		// Token: 0x0400019F RID: 415
		internal const uint ERROR_DEPENDENCY_NOT_FOUND = 5002U;

		// Token: 0x040001A0 RID: 416
		internal const uint ERROR_RESOURCE_NOT_FOUND = 5007U;

		// Token: 0x040001A1 RID: 417
		internal const uint ERROR_OBJECT_ALREADY_EXISTS = 5010U;

		// Token: 0x040001A2 RID: 418
		internal const uint ERROR_RESOURCE_PROPERTIES_STORED = 5024U;

		// Token: 0x040001A3 RID: 419
		internal const int ERROR_CLUSTER_NODE_NOT_FOUND = 5042;

		// Token: 0x040001A4 RID: 420
		internal const uint ERROR_CLUSTER_NETWORK_NOT_FOUND = 5045U;

		// Token: 0x040001A5 RID: 421
		internal const uint ERROR_CLUSTER_INVALID_NETWORK = 5054U;

		// Token: 0x040001A6 RID: 422
		internal const int ERROR_CLUSTER_NODE_ALREADY_MEMBER = 5065;

		// Token: 0x040001A7 RID: 423
		internal const uint ERROR_CLUSTER_EVICT_WITHOUT_CLEANUP = 5896U;

		// Token: 0x040001A8 RID: 424
		internal const uint REGDB_E_CLASSNOTREG = 2147746132U;

		// Token: 0x040001A9 RID: 425
		internal const uint RPC_E_DISCONNECTED = 2147549448U;
	}
}
