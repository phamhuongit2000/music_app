export const SERVER_DOMAIN = "https://7a53-42-116-206-118.ngrok-free.app";

let albumId = null;

export const setAlbumId = (id) => {
  albumId = id;
};

export const getAlbumId = () => {
  return albumId;
};

let singerId = null;

export const setSingerId = (id) => {
    singerId = id;
};

export const getSingerId = () => {
  return singerId;
};