/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { EntryData } from '../models/EntryData';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class GetSpecificEntryHandlerService {

    /**
     * @param entryToGetId 
     * @returns EntryData Success
     * @throws ApiError
     */
    public static getApiEntry(
entryToGetId: number,
): CancelablePromise<EntryData> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/entry/{entryToGetId}',
            path: {
                'entryToGetId': entryToGetId,
            },
            errors: {
                404: `Not Found`,
            },
        });
    }

}
