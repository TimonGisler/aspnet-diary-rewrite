/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { UpdateEntryCommand } from '../models/UpdateEntryCommand';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class UpdateEntryHandlerService {

    /**
     * @param entryToUpdateId 
     * @param requestBody 
     * @returns any Success
     * @throws ApiError
     */
    public static postApiEntry(
entryToUpdateId: number,
requestBody: UpdateEntryCommand,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/entry/{entryToUpdateId}',
            path: {
                'entryToUpdateId': entryToUpdateId,
            },
            body: requestBody,
            mediaType: 'application/json',
        });
    }

}
