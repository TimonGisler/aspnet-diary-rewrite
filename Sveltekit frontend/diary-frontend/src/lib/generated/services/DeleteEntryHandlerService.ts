/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class DeleteEntryHandlerService {

    /**
     * @param entryToDeleteId 
     * @returns any Success
     * @throws ApiError
     */
    public static deleteApiEntry(
entryToDeleteId: number,
): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/entry/{entryToDeleteId}',
            path: {
                'entryToDeleteId': entryToDeleteId,
            },
        });
    }

}
