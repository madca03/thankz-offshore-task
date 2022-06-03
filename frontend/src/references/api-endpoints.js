export const GET_USERS = '/api/users'
export const GET_USER_BY_ID = (userId) => `/api/users/${userId}`
export const CREATE_USER = '/api/users'
export const DELETE_USER = (userId) => `/api/users/${userId}`
export const UPDATE_USER = (userId) => `/api/users/${userId}`
export const CREATE_USERS_FROM_FILE = '/api/users/addfromfile'

export default {
  GET_USERS,
  GET_USER_BY_ID,
  CREATE_USER,
  DELETE_USER,
  UPDATE_USER,
  CREATE_USERS_FROM_FILE
}
