import "./home.css"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faClipboardUser, faCompass, faStar, faArrowLeft, faArrowRight, faMagnifyingGlass, faUser, faMusic, faAnglesRight, faCompactDisc, faRecordVinyl, faMicrophoneLines, faGripVertical, faClockRotateLeft, faUpload, faRightFromBracket, faRedo, faStepBackward, faPause, faPlay, faStepForward, faRandom, faVolumeXmark, faVolumeLow, faVolumeHigh, faHouseMedicalCircleExclamation } from "@fortawesome/free-solid-svg-icons";
import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useNavigate } from "react-router-dom";
import config from "../config";

function AdminAlbumInfo() {
    const dataLocal = localStorage.getItem('user');
    const userLocal  = JSON.parse(dataLocal);
    const navigate = useNavigate();
    const $ = document.querySelector.bind(document);
    const $$ = document.querySelectorAll.bind(document);

    const playlist = $('.list-song');
    const cdThumb = $('.current-song .cd-thumb')
    const songName = $('.current-song .song-name')
    const btnNext = $('.current-song .btn-next')
    const btnPrev = $('.current-song .btn-prev')
    const btnRepeat = $('.current-song .btn-repeat')
    const btnRandom = $('.current-song .btn-random')
    const singgerName = $('.current-song .singger-name')
    const progress = $('.current-song .progress')
    const playBtn = $('.btn-toggle-play')
    const currentSong = $('.current-song')
    const volume = $('.current-song-volume .volume-icon')
    const audio = $('#audio')
    const progressVolume = $('#progress-volume')

    let isRandom = false;
    let isRepeat = false;

    const [songs, setSongs] = useState([]);
    const [albums, setAlbums] = useState({});
    const [singers, setSingers] = useState([]);
    const [top100, setTop100] = useState([]);
    const [albumInfo, setAlbumInfo] = useState([]);

    const [searchKey, setSearchKey] = useState([]);
    const [currentIndex, setCurrentIndex] = useState(0);
    const [isplaying, setIsplaying] = useState(false);

    function handelSearch() {
        const setJsonData=JSON.stringify(searchKey);
		localStorage.setItem('search', setJsonData);
        navigate("/search", { replace: true });
    }

    const albumChooseLocal = localStorage.getItem('albumChoose');
    const albumChooseID  = JSON.parse(albumChooseLocal);

    useEffect(() => {
        fetch(`${config.serverDomain}/Album/GetById/${albumChooseID}`)
        .then(res => res.json())
        .then(data =>{
            setAlbumInfo(data);
            setSongs(data.listSongsInfo)
            console.log(data)
        })
    },[])

    useEffect(() => {
        fetch(`${config.serverDomain}/songs`)
        .then(res => res.json())
        .then(data =>{
            setSongs(data);
        })
    },[])

    useEffect(() => {
        fetch(`${config.serverDomain}/albums`)
        .then(res => res.json())
        .then(data =>{
            setAlbums(data);
        })
    },[])

    useEffect(() => {
        fetch(`${config.serverDomain}/singers`)
        .then(res => res.json())
        .then(data =>{
            setSingers(data);
        })
    },[])

    useEffect(() => {
        fetch(`${config.serverDomain}/top100`)
        .then(res => res.json())
        .then(data =>{
            setTop100(data);
        })
    },[])

    // Xử lí khi bấm play
    function playSong() {
        if(isplaying) {
            audio.pause();
        } else {
            audio.play();
        }
    }

    // Xử lí next bài
    function nextSong() {
        setCurrentIndex(currentIndex + 1)
    }

    // Xử lí prev bài
    function prevSong() {
        setCurrentIndex(currentIndex - 1)
    }
    
    // Xử lí khi đang play
    function audioPlay() {
        setIsplaying(true);
        currentSong.classList.add('playing');
    }

    // Xử lí khi tiến độ bài hát thay đổi
    function onTimeUpdate() {
        if(audio.duration) {
           const progressPercent = Math.floor(audio.currentTime / audio.duration * 100);
           progress.value = progressPercent;
        }
    }

    // Xử lí khi tua
    function onChangeCurentTime(e) {
        const seekTime = Math.floor(e.target.value * audio.duration / 100);
        audio.currentTime = seekTime;
    }

    // Xử lí khi đang pause
    function audioPause() {
        setIsplaying(false);
        currentSong.classList.remove('playing');
    }

    // Chọn bài hát
    function choeseSong(index) {
        setCurrentIndex(index)
    }

    // Handle logout
    function handleLogout() {
        localStorage.removeItem('user');
        navigate("/Home", { replace: true });
    }

    return (
        <div className="root">
            <div className="container">
                {/* Header */}
                <div className="header">
                    <div className="flex-left">
                        {/* Back and next button */}
                        <div className="header-control">
                            <div className="btn btn-back">
                                <i><FontAwesomeIcon icon={faArrowLeft}></FontAwesomeIcon></i>
                            </div>
                            <div className="btn btn-next">
                                <i><FontAwesomeIcon icon={faArrowRight}></FontAwesomeIcon></i>
                            </div>
                        </div>

                        {/* Search */}
                        <div className="search">
                            <i onClick={() => handelSearch()}><FontAwesomeIcon icon={faMagnifyingGlass}></FontAwesomeIcon></i>
                            <div className="search-content">
                                <input className="search-input" type="text" placeholder="Nhập tên bài hát cần tìm kiếm..."
                                    onChange={(e) => setSearchKey(e.target.value)}
                                />
                            </div>
                        </div>
                    </div>

                    {/* Login Register */}
                    <div className="header-user_name">
                        {userLocal?.name || ""}
                    </div>
                </div>
                {/* End header */}
                {/* Side bar */}
                <div className="side-bar">
                <div className="side-bar-wrapper">
                    {/* Logo */}
                    <div className="logo-wrapper">
                        <div className="logo">
                            <i><FontAwesomeIcon icon={faMusic}></FontAwesomeIcon></i>
                        </div>
                        MP3
                    </div>

                    {/* Guest nav bar */}
                    <div className="side-bar-main">
                        {/* Nav bar main */}
                        <div className="nav-bar nar-bar-main">
                            <li className="nav-bar-item">
                                <a href="/adminsongs" className="title">
                                    <i><FontAwesomeIcon icon={faMusic}></FontAwesomeIcon></i>
                                    <span>QL bài hát</span>
                                </a>
                            </li>
                            <li className="nav-bar-item">
                                <a href="/adminsinger" className="title">
                                    <i><FontAwesomeIcon icon={faClipboardUser}></FontAwesomeIcon></i>
                                    <span>QL ca sĩ</span>
                                </a>
                            </li>
                            <li className="nav-bar-item active">
                                <a href="/adminalbum" className="title">
                                    <i><FontAwesomeIcon icon={faCompactDisc}></FontAwesomeIcon></i>
                                    <span>QL album</span>
                                </a>
                            </li>
                            {/* <li className="nav-bar-item">
                                <a href="/adminuser" className="title">
                                    <i><FontAwesomeIcon icon={faUser}></FontAwesomeIcon></i>
                                    <span>QL User</span>
                                </a>
                            </li> */}
                        </div>
                    </div>

                    {/* Log out */}
                    <div className="log-out">
                        <div className="nav-bar nar-bar-user">
                            <li className="nav-bar-item">
                                <Link to="/" className="title">
                                    <i><FontAwesomeIcon icon={faRightFromBracket}></FontAwesomeIcon></i>
                                    <span>Đăng xuất</span>
                                </Link>
                            </li>
                        </div>
                    </div>
                </div>
                </div>
                {/* End side bar */}


                {/* Main */}
                <div className="main">
                    <div className="home-main">
                        <div className="ablum-info-wrap">
                            <div className="album-info-header">
                                <div className="album-info-image-wrap">
                                    <div className="album-info-image"
                                        style={{backgroundImage: `url(${albumInfo?.imgUrl || ""})`}}>
                                    </div>
                                </div>
                                <div className="album-info-introduce">
                                    <div className="album-name">{albumInfo?.name || ""}</div>
                                </div>
                            </div>
                            <div className="list-song">
                                {songs.map((song, index) => {
                                    return (
                                        <div className={`song-info-container ${currentIndex == index ? 'active': ''} w-1`} key={index} data-index = {index}>
                                            <div className="current-song-info w-3">
                                                <div className="cd" onClick={() => {
                                                        // if(userLocal[0].level != 'vip' && song.vip == 'Vip') {
                                                        //     alert('Bài này cần mất phí để nghe bấm oke để chuyển sang trang đăng kí thành viên vip')
                                                        //     navigate("/registerVip", { replace: true });
                                                        // }
                                                        // else {
                                                        //     choeseSong(index)
                                                        // }

                                                        choeseSong(index)
                                                    }}>
                                                    <div className="cd-thumb"
                                                        style={{backgroundImage: `url(${song?.imgUrl || ""})`}}>
                                                    </div>
                                                </div>
                                                <div className="song-info">
                                                    <div className="song-name">
                                                        {song.name}
                                                        {/* <span className={`card ${song.vip == "Vip"?'vip-member':'normal-member'}`}>
                                                            {`${song.vip == "Vip"? 'Vip': ''}`}
                                                        </span> */}
                                                    </div>
                                                    <div className="singger-name">
                                                        {song?.singerName || ""}
                                                    </div>
                                                </div>
                                            </div>

                                            <div className="song-title w-3">
                                                {song.description}
                                            </div>

                                            <div className="song-time w-3">
                                                {`0${Math.floor(Math.random()*2) + 3}:${Math.floor(Math.random()*50) + 10}`}
                                            </div>
                                        </div>
                                    )
                                })}
                            </div>
                        </div>
                    </div>
                </div>
                {/* End content */}

                {/* Footer */}
                <div className="footer">

                </div>
            </div>
        </div> 
    )
}

export default AdminAlbumInfo;